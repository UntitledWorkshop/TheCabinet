using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Gameplay.Rating
{
    public class RatingModificator : MonoBehaviour
    {
        [SerializeField] private float baseMultiplier;
        [SerializeField] private InputActionAsset playerInputAction;

        public void ApplyModifier()
        {
            ISingleton<RatingController>.Instance.Gain(baseMultiplier);
        }

        public void DeclineModifier()
        {
            ISingleton<RatingController>.Instance.Gain(-baseMultiplier);
        }

        private void OnEnable()
        {
            playerInputAction.Enable();
            playerInputAction["rating_apply"].Enable();
            playerInputAction["rating_decline"].Enable();
        }

        private void OnDisable()
        {
            playerInputAction.Disable();
            playerInputAction["rating_apply"].Disable();
            playerInputAction["rating_decline"].Disable();
        }

        private void Start()
        {
            playerInputAction["rating_apply"].performed += _ =>  ApplyModifier();
            playerInputAction["rating_decline"].performed += _ =>  DeclineModifier();
        }
    }
}
