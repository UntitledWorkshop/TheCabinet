using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Settings
{
    [Serializable]
    public class SettingsHolder
    {
        [SerializeField] private UnityEvent onSettingsSaved;
        [SerializeField] private UnityEvent onSettingsLoaded;

        public Dictionary<string, float> Settings = new();

        public void Initialize()
        {
            Settings.Add("master", 0);
            Settings.Add("music", 0);
            Settings.Add("sfx", 0);
            Settings.Add("ui", 0);
            Settings.Add("mouse", 0);
        }

        public void Save()
        {
            foreach (var kv in Settings)
            {
                PlayerPrefs.SetFloat(kv.Key, kv.Value);
            }
            
            onSettingsSaved.Invoke();
        }

        public void Load()
        {            
            foreach (var kv in Settings)
            {
                Settings[kv.Key] = PlayerPrefs.GetFloat(kv.Key, kv.Value);
            }
            
            onSettingsLoaded.Invoke();
        }
    }
}