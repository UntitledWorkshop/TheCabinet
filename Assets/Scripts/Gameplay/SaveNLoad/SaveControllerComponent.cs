using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Gameplay.SaveNLoad
{
    public class SaveControllerComponent : MonoBehaviour, ISingleton<SaveControllerComponent>
    {
        [SerializeField] private string fileName;
        [SerializeField] private string filePath;

        public void Save()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SaveController.SaveGame(filePath + "/" + fileName, sceneName);
        }
        
        public void Load()
        {
            if (SaveController.LoadGame(filePath + "/" + fileName, out var sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (filePath == "")
                filePath = Application.persistentDataPath;
        }

        private void OnDestroy()
        {
            Save();
        }
        
        private void OnEnable()
        {
            ISingleton<SaveControllerComponent>.Instance = this;
        }

        private void OnDisable()
        {
            ISingleton<SaveControllerComponent>.Instance = null;
        }
    }
}
