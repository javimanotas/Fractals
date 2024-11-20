using Fractals.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Fractals.UI
{
    /// <summary> Allows the user to select one choice from multiple ones </summary>
    public class RadioButton : MonoBehaviour, IPointerClickHandler
    {
        /// <summary> Represents what of the radio buttons is selected by beeing of the same position <para>
        /// All RadioButtons with reference of the same SharedImage belong to the same group </para> </summary>
        [SerializeField] RectTransform SharedImage;

        [SerializeField] UnityEvent OnClick;

        RectTransform _rect;

        void Awake()
        {
            _rect = GetComponent<RectTransform>();

            OnClick.AddListener(() =>
            {
                SharedImage.position = _rect.position;

                // SharedImage popup animation
                AnimationController.Instance.AddAnimation(new(
                    cancellationToken: destroyCancellationToken,
                    time: 0.4f,
                    backwards: false,
                    update: f => SharedImage.localScale = Vector2.one * f,
                    easing: Easings.OutBack(amplitude: 5)
                ));
            });
        }

        public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke();
    }
}