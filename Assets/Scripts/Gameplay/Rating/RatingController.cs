using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Gameplay.Rating
{
    public class RatingController : MonoBehaviour, ISingleton<RatingController>
    {
        public enum RatingState
        {
            Success,
            Fail,
            Neutral
        }
        
        [SerializeField, Range(0, 100)] private float ratingMax;
        [SerializeField, Range(0, 100)] private float ratingGainBase;
        [SerializeField] private UnityEvent onRatingCompleted;
        [SerializeField] private UnityEvent onRatingChange;
        
        public float Rating { get; private set; }

        public RatingState GetRatingState()
        {
            if (Rating <= 0)
                return RatingState.Success;
            
            return Rating >= ratingMax ? RatingState.Fail : RatingState.Neutral;
        }
        
        public void Gain(float mul)
        {
            Rating +=  mul * ratingGainBase;

            if (Rating <= 0 || Rating >= ratingMax)
                onRatingCompleted.Invoke();
            onRatingChange.Invoke();
        }
        
        private void OnEnable()
        {
            ISingleton<RatingController>.Instance = this;
        }

        private void OnDisable()
        {
            ISingleton<RatingController>.Instance = null;
        }
    }
}
