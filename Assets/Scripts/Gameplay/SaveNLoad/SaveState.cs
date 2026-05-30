using UnityEngine;


namespace Gameplay.SaveNLoad
{
    /**
     * Plain C# class that contains save data
     */
    public class SaveState
    {
        [SerializeField] public string CurrentSceneName;
        [SerializeField] public float CurrentRatingValue;
    }
}
