using Fractals.Animations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Animates a single panel from the menu </summary>
    [RequireComponent(typeof(RectTransform))]
    public class PanelAnimation : MonoBehaviour
    {
        static readonly List<PanelAnimation> _panels = new();

        public static IEnumerable<PanelAnimation> Panels => _panels;

        /// <summary> The order in which the animation will start </summary>
        [SerializeField] int SortOrder, ReverseSortOrder;

        [SerializeField] bool Vertical = true;

        RectTransform _rect;

        Vector2 _targetPosition;

        Vector2 _hidedPosition;

        const float _ANIMATION_TIME = 0.5f;

        bool _focused = false;

        void Start()
        {
            _rect = GetComponent<RectTransform>();
            _hidedPosition = _rect.anchoredPosition;
            _targetPosition = _rect.anchoredPosition - (Vertical ? Vector2.up * 1266 : Vector2.right * (_rect.anchoredPosition.x * 2));
            _panels.Add(this);
        }

        void OnDestroy() => _panels.Remove(this);

        public async Task Animate(bool show)
        {
            await Task.Delay((show ? SortOrder : ReverseSortOrder) * (Vertical ? 15 : 25));

            Func<float, float> easing = show ? Easings.OutBack(amplitude: Vertical ? 1 : 2) : Easings.SmoothStep;

            await AnimationController.Instance.AddAsyncAnimation(new(
                cancellationToken: destroyCancellationToken,
                time: _ANIMATION_TIME,
                backwards: !show,
                update: f => _rect.anchoredPosition = Vector2.LerpUnclamped(_hidedPosition, _targetPosition, f),
                easing: easing
            ));
        }

        /// <summary> Used when focusing a panel </summary>
        public async Task ToggleScale(bool isParent)
        {
            if (!isParent)
            {
                await AnimationController.Instance.AddAsyncAnimation(new(
                    cancellationToken: destroyCancellationToken,
                    time: _ANIMATION_TIME,
                    backwards: !_focused,
                    update: f => _rect.localScale = Vector2.one * f,
                    easing: _focused ? Easings.SmoothStep : Easings.OutBack(1)
                ));
            }
            else
            {
                var desiredPos = new Vector2(-1194.5f, 645) + _rect.sizeDelta * new Vector2(0.5f, -0.5f);

                await AnimationController.Instance.AddAsyncAnimation(new(
                    cancellationToken: destroyCancellationToken,
                    time: _ANIMATION_TIME,
                    backwards: !_focused,
                    update: f => _rect.anchoredPosition3D = Vector3.Lerp(desiredPos, _targetPosition, f).With(z: 0),
                    easing: Easings.SmoothStep
                ));
            }   

            _focused = !_focused;
        }
    }
}