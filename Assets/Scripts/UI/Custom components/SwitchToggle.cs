using Fractals.Animations;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Custom implementation of a toggle <para>
    /// Moves the child image left-right instead of hiding-showing it</para> </summary>
    [RequireComponent(typeof(Image))]
    public class SwitchToggle : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] UnityEvent<bool> OnSwitch;

        public bool IsTurnedOn { get; private set; } = false;

        Image _image;

        /// <summary> The image that moves to mark if it's turned on </summary>
        RectTransform _child;

        float _childLocalX;

        const float _ANIMATION_TIME = 0.22f;

        void Start()
        {
            _image = GetComponent<Image>();
            _image.color = _image.color.WithAlpha(0.01f);

            _child = GetComponentsInChildren<RectTransform>()
                .First(x => x.gameObject != gameObject);

            _childLocalX = _child.anchoredPosition.x;
        }

        public void TurnOn()
        {
            if (!IsTurnedOn)
            {
                OnPointerClick(null);
            }
        }

        public void SetOn(bool on)
        {
            if (on != IsTurnedOn)
            {
                OnPointerClick(null);
            }
        }

        public void OnPointerClick(PointerEventData _)
        {
            // Image animation
            AnimationController.Instance.AddAnimation(new(
                cancellationToken: destroyCancellationToken,
                time: _ANIMATION_TIME,
                backwards: IsTurnedOn,
                update: f => _image.color = _image.color.WithAlpha(f + 0.01f) // +0.01f To detect raycast
            ));

            // Move animation
            AnimationController.Instance.AddAnimation(new(
                cancellationToken: destroyCancellationToken,
                time: _ANIMATION_TIME,
                backwards: IsTurnedOn,
                update: f => _child.anchoredPosition = _child.anchoredPosition.With(x: Mathf.Lerp(_childLocalX, -_childLocalX, f)),
                easing: Easings.SmoothStep
            ));

            IsTurnedOn = !IsTurnedOn;
            OnSwitch?.Invoke(IsTurnedOn);
        }
    }
}