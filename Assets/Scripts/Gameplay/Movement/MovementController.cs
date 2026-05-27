using System;
using Dialogue;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;


namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private InputAction iaLook;
        [SerializeField] private Camera mainCamera;
        
        // TODO: Delegate to settings controller? 
        [SerializeField, Range(0, 1)] private float mouseSensitivity;
        [SerializeField] private Vector2 clampAngles;
        
        private Vector3 _cameraAnglesComputed;
        
        #region COMPONENT_TOGGLE
        private void OnEnable()
        {
            iaLook.Enable();
        }

        private void OnDisable()
        {
            iaLook.Disable();
        }
        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _cameraAnglesComputed = mainCamera.transform.localEulerAngles;
            
            GetComponent<DialogueProvider>()?.TriggerDialogue();
        }

        private void Update()
        {
            var lookInputValue = iaLook.ReadValue<Vector2>() * mouseSensitivity;
            
            // We should precompute camera angles because Unity Transform type does not store negative angle values
            // Well... fuck it.
            _cameraAnglesComputed = new Vector3(
                Math.Clamp(_cameraAnglesComputed.x - lookInputValue.y, clampAngles.x, clampAngles.y), 
                _cameraAnglesComputed.y + lookInputValue.x, 
                0);
            
            mainCamera.transform.localEulerAngles = _cameraAnglesComputed;
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            mainCamera = Camera.main;
        }
        #endif
    }
}