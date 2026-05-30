using Gameplay.SaveNLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace UI
{
    public class MainMenuFunctions: MonoBehaviour
    {
        [SerializeField] private string startSceneName;

        public void NewGame()
        {
            SceneManager.LoadScene(startSceneName);
        }
        
        public void Load()
        {
            ISingleton<SaveControllerComponent>.Instance.Load();
        }
    }
}
