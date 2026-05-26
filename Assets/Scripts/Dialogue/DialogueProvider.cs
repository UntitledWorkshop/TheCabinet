using UnityEngine;
using Utility;

namespace Dialogue {
    public class DialogueProvider : MonoBehaviour
    {
        [Header("Test Settings")]
        [SerializeField] private string[] textSequence;

        public void TriggerDialogue()
        {
            var controllerInstance = ISingleton<DialogueOutputController>.Instance;
            controllerInstance?.PushMany(textSequence);
        }
    }
}
