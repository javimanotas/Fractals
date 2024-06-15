using Fractals.Animations;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fractals.UI
{
    /// <summary> Custom animations for a button </summary>
    public abstract class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        const float _ANIMATION_TIME = 0.2f;

        protected abstract Action<float> ClickAnimation { get; }

        protected abstract Action<float> HoverAnimation { get; }

        public void OnPointerClick(PointerEventData eventData) => AnimationController.Instance.AddAnimation(new(
            cancellationToken: destroyCancellationToken,
            time: _ANIMATION_TIME,
            backwards: false,
            update: ClickAnimation,
            easing: Easings.InOutParabola
        ));

        // OnPointerEnter and OnPointerExit use the same field
        AnimatedField HoverField(bool enter) => new(
            cancellationToken: destroyCancellationToken,
            time: _ANIMATION_TIME,
            backwards: !enter,
            update: HoverAnimation
        );

        public void OnPointerEnter(PointerEventData eventData) => AnimationController.Instance.AddAnimation(HoverField(true));

        public void OnPointerExit(PointerEventData eventData) => AnimationController.Instance.AddAnimation(HoverField(false));
    }
}