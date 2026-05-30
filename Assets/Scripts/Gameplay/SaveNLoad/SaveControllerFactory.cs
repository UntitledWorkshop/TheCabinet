using UnityEngine;
using Utility;

namespace Gameplay.SaveNLoad
{
    public class SaveControllerFactory : MonoBehaviour
    {
        [SerializeField] private GameObject saveControllerPrefab;

        private void Awake()
        {
            if (FindAnyObjectByType<SaveControllerComponent>() == null)
            {
                Instantiate(saveControllerPrefab);
            }
        }
    }
}
