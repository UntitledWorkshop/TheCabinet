using System.Collections;
using Dialogue;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

using Gameplay;
using Gameplay.Dialogue;
using NUnit.Framework;
using Utility;

namespace Tests.Unit_Tests.Gameplay {
    public class TestDialogues
    {
        [UnityTest]
        public IEnumerator TestDialogueSimpleOutput()
        {
            SceneManager.LoadScene("TestDialogue", LoadSceneMode.Additive);
            yield return null;

            var controller = Object.FindAnyObjectByType<DialogueOutputController>();
            
            // Ensure we're ready to go
            Assert.IsTrue(controller.didAwake);
            Assert.IsNotNull(ISingleton<DialogueOutputController>.Instance);

            const string text = "Hello World!";
            controller.Push(text);

            foreach (var _ in text)
            {
                yield return null;
            }
                
            Assert.IsTrue(controller.Empty);
        }
    }
}