using System.Globalization;
using Gameplay.Rating;
using TMPro;
using UnityEngine;
using Utility;

namespace Gameplay.UI
{
    /**
     * Simple text viewer for rating value
     */
    public class RatingViewModel : MonoBehaviour
    {
        [SerializeField] public TMP_Text ratingText;
        
        public void OnRatingChanged()
        {
            var currRating = RatingController.PersistentRating;
            ratingText.text = currRating.ToString(CultureInfo.InvariantCulture);
        }
    }
}
