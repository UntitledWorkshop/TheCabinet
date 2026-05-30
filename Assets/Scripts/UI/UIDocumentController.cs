using UnityEngine;

namespace UI
{
    public class UIDocumentController : MonoBehaviour
    {
        [SerializeField] private ButtonController[] buttons;

        private void Start()
        {
            foreach (ButtonController controller in buttons)
                controller.Initialize();
        }
    }
}