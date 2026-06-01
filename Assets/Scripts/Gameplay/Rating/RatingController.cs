using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Gameplay.Rating
{
    public class RatingController : MonoBehaviour, ISingleton<RatingController>
    {
        public static float PersistentRating = 0;
        
        public enum RatingState
        {
            Success,
            Fail,
            Neutral
        }
        
        [SerializeField, Range(0, 100)] private float ratingMax;
        [SerializeField, Range(0, 100)] private float ratingGainBase;
        [SerializeField, Range(1, 10)] private uint ratingGainCount;
        [SerializeField] private UnityEvent onRatingCompleted;
        [SerializeField] private UnityEvent onRatingChange;

        private uint _currentRatingGainCount = 0;
        
        public RatingState GetRatingState()
        {
            if (PersistentRating <= 0)
                return RatingState.Success;
            
            return PersistentRating >= ratingMax ? RatingState.Fail : RatingState.Neutral;
        }

        public void Set(float? rating)
        {
            if (!rating.HasValue)
                return;
            
            PersistentRating = rating.Value;

            if (PersistentRating <= 0 || PersistentRating >= ratingMax)
                onRatingCompleted.Invoke();
            
            onRatingChange.Invoke();
        }
        
        public void Gain(float mul)
        {
            _currentRatingGainCount++;
            Set(mul * ratingGainBase);

            if (_currentRatingGainCount >= ratingGainCount)
            {
                onRatingCompleted.Invoke();
            }
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
