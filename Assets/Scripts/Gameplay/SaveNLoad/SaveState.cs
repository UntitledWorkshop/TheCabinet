using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Gameplay.SaveNLoad
{
    /**
     * Plain C# class that contains save data
     */
    [Serializable]
    public class SaveState
    {
        [SerializeField] public string currentSceneName;
        [SerializeField] public float currentRatingValue;
    }
}
