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
        [SerializeField] private UnityEvent onRatingCompleted;
        [SerializeField] private UnityEvent onRatingChange;
        
        public float Rating { get; private set; }

        public RatingState GetRatingState()
        {
            if (Rating <= 0)
                return RatingState.Success;
            
            return Rating >= ratingMax ? RatingState.Fail : RatingState.Neutral;
        }

        public void Set(float? rating)
        {
            if (!rating.HasValue)
                return;
            
            Rating = rating.Value;

            if (Rating <= 0 || Rating >= ratingMax)
                onRatingCompleted.Invoke();
            onRatingChange.Invoke();
        }
        
        public void Gain(float mul)
        {
            Set(mul * ratingGainBase);
        }
        
        private void OnEnable()
        {
            ISingleton<RatingController>.Instance = this;
        }

        private void OnDisable()
        {
            ISingleton<RatingController>.Instance = null;
        }

        private void OnDestroy()
        {
            PersistentRating = Rating;
        }

        private void Awake()
        {
            Set(PersistentRating);
        }
    }
}
