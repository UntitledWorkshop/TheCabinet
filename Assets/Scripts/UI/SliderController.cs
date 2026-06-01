using System;
using Gameplay.Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Utility;

namespace UI
{
    [Serializable]
    public class SliderController
    {
        [Header("UI Setup")] 
        [SerializeField] private string id;
        [SerializeField] private UIDocument document;
        [Header("Actions")] 
        [SerializeField] private UnityEvent onValueChanged;

        public void Initialize()
        {
            var slider = document.rootVisualElement.Q<Slider>(id);
            slider.RegisterValueChangedCallback(ValueChanged);
        }

        public void ValueChanged(ChangeEvent<float> ctx)
        {
            if (!ISingleton<SettingsController>.Instance)
                return;

            ISingleton<SettingsController>.Instance.Holder.Settings[id] = ctx.newValue;
            onValueChanged.Invoke();
        }
    }
}