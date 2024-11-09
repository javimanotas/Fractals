using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Fractals.Animations;

namespace Fractals.UI
{
    /// <summary> Transitions on and off the post-processing effect: depth of field </summary>
    public class Blur : MonoBehaviour
    {
        DepthOfField _depthOfField = null;

        void Start()
        {
            var volume = FindFirstObjectByType<Volume>();

            if (volume.profile.TryGet<DepthOfField>(out var depthOfField))
            {
                _depthOfField = depthOfField;
            }
        }

        public void Toggle() => AnimationController.Instance.AddAnimation(new(
            cancellationToken: destroyCancellationToken,
            time: 0.5f,
            backwards: _depthOfField.focalLength.value < 40,
            update: f => _depthOfField.focalLength.value = Mathf.Lerp(45, 1, f))
        );
    }
}