using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UI
{
    [Serializable]
    public class ButtonController {
        [Header("UI Setup")]
        [SerializeField] private string id;
        [SerializeField] private UIDocument document;
        [Header("Action")]
        [SerializeField] private UnityEvent action = new UnityEvent();
        [SerializeField] private UnityEvent hover = new UnityEvent();

        public void Initialize()
        {
            var btn = document.rootVisualElement.Q<Button>(id);
            btn.RegisterCallback((ClickEvent _) => { InvokeAction(action); });
            btn.RegisterCallback((MouseOverEvent _) => { InvokeAction(hover); });
        }

        private void InvokeAction(UnityEvent callback)
        {
            callback.Invoke();
        }
    }
}