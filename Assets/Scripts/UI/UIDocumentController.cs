using UnityEngine;

namespace UI
{
    public class UIDocumentController : MonoBehaviour
    {
        [SerializeField] private ButtonController[] buttons;

        private void Start()
        {
            foreach (var controller in buttons)
                controller.Initialize();
        }
    }
}