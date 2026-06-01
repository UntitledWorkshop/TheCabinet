using UnityEngine;
using Utility;

namespace Gameplay.Settings
{
    public class SettingsController : MonoBehaviour, ISingleton<SettingsController>
    {
        [SerializeField] private SettingsHolder settingsHolder;
        public SettingsHolder Holder => settingsHolder;

        public void Awake()
        {
            Holder.Initialize();
        }
        
        public void Load()
        {
            settingsHolder.Load();
        }

        public void Save()
        {
            settingsHolder.Save();
        }
        
        private void OnEnable()
        {
            ISingleton<SettingsController>.Instance = this;
        }

        private void OnDisable()
        {
            ISingleton<SettingsController>.Instance = null;
        }
    }
}
