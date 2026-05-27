using System;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.Interactables
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onInteract;
        [SerializeField] private InputActionAsset playerInput;

        private bool _canInteract = false;
        
        private void OnMouseEnter()
        {
            _canInteract = true;
        }

        private void OnMouseExit()
        {
            _canInteract = false;
        }

        public void Interact()
        {
            if (_canInteract)
                onInteract.Invoke();
        }

        private void Awake()
        {
            playerInput["interact"].performed += _ => Interact();
        }

        private void OnEnable()
        {
            if (!playerInput)
                return;

            playerInput.Enable();
            playerInput["interact"].Enable();
        }

        private void OnDisable()
        {
            if (!playerInput)
                return;
            
            playerInput.Disable();
            playerInput["interact"].Disable();
        }
    }
}
