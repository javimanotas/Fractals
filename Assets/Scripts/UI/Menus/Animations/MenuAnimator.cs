using Fractals.Animations;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Shows and hides the menu </summary>
    public class MenuAnimator : MonoBehaviour
    {
        bool _isHided = true;

        // Arrow of the button
        [SerializeField] RectTransform Arrow;

        [SerializeField] Blur Blur;

        [SerializeField] RawImage FractalImage;

        bool _isAnimating = false;

        public void ToggleEffects()
        {
            AnimationController.Instance.AddAnimation(new(
                cancellationToken: destroyCancellationToken,
                time: 0.4f,
                backwards: FractalImage.color.r < 0.7f,
                update: f => FractalImage.color = (Color.white * Mathf.Lerp(1, 0.5f, f)).WithAlpha(1)
            ));

            Blur.Toggle();
        }

        public async void Toggle()
        {
            if (_isAnimating)
            {
                return;
            }

            _isAnimating = true;

            var backWards = _isHided;

            ToggleEffects();

            // Arrow spin animation
            AnimationController.Instance.AddAnimation(new(
                cancellationToken: destroyCancellationToken,
                time: 0.2f,
                backwards: false,
                update: f =>
                    Arrow.eulerAngles = Vector3.forward * Mathf.Lerp(backWards ? 180 : 0, backWards ? 360 : 180, f),
                easing: Easings.SmoothStep
            ));

            var tasks = PanelAnimation.Panels
                .Select(x => x.Animate(backWards));

            await Task.WhenAll(tasks);
            
            _isAnimating = false;
            _isHided = !_isHided;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Toggle();
            }
        }
    }
}