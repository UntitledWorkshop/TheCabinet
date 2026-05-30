using System.Collections;
using Gameplay.Rating;
using Gameplay.SaveNLoad;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Utility;

public class TestSaveLoad
{
    [UnityTest]
    public IEnumerator TestSaveLoadWithEnumeratorPasses()
    {
        SceneManager.LoadScene("TestDialogue", LoadSceneMode.Single);
        yield return null;

        Assert.IsNotNull(ISingleton<SaveControllerComponent>.Instance);
        Assert.IsNotNull(ISingleton<RatingController>.Instance);
        
        var saveController =  ISingleton<SaveControllerComponent>.Instance;
        var ratingController =  ISingleton<RatingController>.Instance;
        
        ratingController.Set(10);
        saveController.Save();
        
        ratingController.Set(25);
        Assert.IsTrue(Mathf.Approximately(ratingController.Rating, 25));
        saveController.Load();
        Assert.IsTrue(Mathf.Approximately(ratingController.Rating, 10));
    }
}
