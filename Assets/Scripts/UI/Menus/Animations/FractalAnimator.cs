using UnityEngine;

namespace Fractals.UI
{
    public class FractalAnimator : MonoBehaviour
    {
        [SerializeField] FractalDispatcher3D Dispatcher;

        [SerializeField] SliderAndInputField Animation;

        bool _animate = false;

        const float _ANIMATION_SPEED = -1 / 7f;

        public void Toggle(bool enable) => _animate = enable;

        public void SetAnimation(float f)
        {
            Dispatcher.Time = f;
        }

        public void Update()
        {
            if (_animate)
            {
                var t = Dispatcher.Time + Time.deltaTime * _ANIMATION_SPEED;

                while (t < 0)
                {
                    t += Mathf.PI / 5;
                }

                Animation.Value = t;
                SetAnimation(t);
            }
        }
    }
}