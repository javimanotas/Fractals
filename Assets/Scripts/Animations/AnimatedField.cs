using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Fractals.Animations
{
    /// <summary> Parameters and behaviour for the animations </summary>
    public class AnimatedField
    {
        /// <summary> Prevents animations from destroyed objects to keep running </summary>
        readonly CancellationToken _cancellationToken;

        float _counter;

        readonly float _targetValue;

        readonly float _time;

        readonly Func<float, float> _easing;

        /// <summary> Is called on every step of the animation </summary>
        readonly Action<float> _update;

        public readonly TaskCompletionSource<bool> Completion = new TaskCompletionSource<bool>();

        public bool Finished
        {
            get => _counter == _targetValue;
            private set => _counter = _targetValue;
        }

        public AnimatedField(CancellationToken cancellationToken, float time, bool backwards, Action<float> update, Func<float, float> easing = null)//, Action endAnimationEvent = null)
        {     
            _cancellationToken = cancellationToken;

            _counter = backwards ? 1 : 0;
            _targetValue = 1 - _counter;

            _time = time;
            _update = update;
            _easing = easing ?? (f => f);
        }

        public void Update(float deltaTime)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                Finished = true;
                return;
            }

            var direction = _targetValue > 0.5f ? 1 : -1;
            _counter += direction * deltaTime / _time;
            _counter = Mathf.Clamp01(_counter);

            _update(_easing(_counter));

            if (Finished)
            {
                Completion.SetResult(true);
            }
        }
    }
}