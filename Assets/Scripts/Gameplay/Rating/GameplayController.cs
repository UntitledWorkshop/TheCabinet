using Gameplay.Rating;
using UnityEngine;
using Utility;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private string successSceneName;
    [SerializeField] private string failSceneName;
    [SerializeField] private string neutralSceneName;

    public void FinishGame()
    {
        var ratingController = ISingleton<RatingController>.Instance;

        switch (ratingController.GetRatingState())
        {
            case RatingController.RatingState.Success:
                SceneManager.LoadScene(successSceneName);
                break;
            case RatingController.RatingState.Fail:
                SceneManager.LoadScene(failSceneName);
                break;
            case RatingController.RatingState.Neutral:
            default:
                SceneManager.LoadScene(neutralSceneName);
                break;
        }
    }
}
