using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utility;

namespace Gameplay.Dialogue {
    public class DialogueOutputController : MonoBehaviour, ISingleton<DialogueOutputController>
    {
        private enum OutputFieldState
        {
            Ready,
            Busy,
            ShouldSkip
        }
        
        [SerializeField, Range(0f, 1f)] private float symbolDelay;
        [SerializeField] private UnityEvent onCharacterDisplay;
        [Header("Linked Items")] 
        [SerializeField]
        private TMP_Text outputField;
        [SerializeField]
        private InputActionAsset playerInput;
        
        private OutputFieldState _currentState = OutputFieldState.Ready;
        private readonly Queue<string> _textQueue = new();

        private void OnTextDisplayFinishedAndReady()
        {
            if (!Empty) 
                StartOutput();
            else 
                outputField.text = "";
        }
        
        private IEnumerator Output(string outputString)
        {
            outputField.text = "";
            
            foreach (var ch in outputString.TakeWhile(ch => _currentState != OutputFieldState.ShouldSkip))
            {
                outputField.text += ch;
                onCharacterDisplay.Invoke();
                yield return new WaitForSeconds(symbolDelay);
            }
            
            outputField.text = outputString;
            
            while (_currentState != OutputFieldState.ShouldSkip)
                yield return new WaitForEndOfFrame();

            _currentState = OutputFieldState.Ready;
            OnTextDisplayFinishedAndReady();
        }

        private void DisplayText(string outputString)
        {
            _currentState = OutputFieldState.Busy;
            StartCoroutine(Output(outputString));
        }

        private void StartOutput()
        {
            if (Busy || Empty) return;
            DisplayText(_textQueue.Dequeue());
        }

        public void Push(string text)
        {
            _textQueue.Enqueue(text);
            StartOutput();
        }

        public void PushMany(string[] texts)
        {
            foreach (var text in texts)
                _textQueue.Enqueue(text);
            StartOutput();
        }

        public bool Empty => _textQueue.Count == 0;
        public bool Busy => _currentState != OutputFieldState.Ready;

        private void Awake()
        {    
            if (ISingleton<DialogueOutputController>.Instance != null && 
                ISingleton<DialogueOutputController>.Instance != this)
            {
                Debug.LogError("DialogueOutputController: Multiple singleton instances are on scene!");
                return;
            }
            
            ISingleton<DialogueOutputController>.Instance = this;
        }

        private void OnDestroy()
        {
            ISingleton<DialogueOutputController>.Instance = null;
        }
        
        private void Start()
        {
            if (!playerInput)
            {
                Debug.LogWarning("DialogueOutputController: playerInput is missing!");
                return;
            }
            
            playerInput["skip"].performed += OnSkipActionPerformed;
        }
        
        #region INPUT

        private void OnSkipActionPerformed(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed || !Busy) return;
            _currentState = OutputFieldState.ShouldSkip;
        }
        
        private void OnEnable()
        {
            ISingleton<DialogueOutputController>.Instance = this;
            if (!playerInput)
                return;
            
            playerInput.Enable();
            playerInput["skip"].Enable();
        }

        private void OnDisable()
        {
            ISingleton<DialogueOutputController>.Instance = null;
            if (!playerInput)
                return;
            
            playerInput["skip"].Disable();
            playerInput.Disable();
        }

        #endregion
    }
}