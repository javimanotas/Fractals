using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Custom implementation of the button tint transition (I don't like the one that unity provides) </summary>
    [RequireComponent(typeof(Image))]
    public class ButtonTint : ButtonAnimation
    {
        Image _image;

        const float _ZERO_ALPHA = 0.001f; // To detect raycast

        const float _HOVER_ALPHA = 0.5f;

        const float _CLICK_AKPHA = 0.7f;

        void Start()
        {
            _image = GetComponent<Image>();
            _image.color = _image.color.WithAlpha(_ZERO_ALPHA);
        }

        Action<float> LerpAlpha(float maxAlpha) => f => _image.color = _image.color.WithAlpha(Mathf.Lerp(_ZERO_ALPHA, maxAlpha, f));

        protected override Action<float> HoverAnimation => LerpAlpha(_HOVER_ALPHA);

        protected override Action<float> ClickAnimation => LerpAlpha(_CLICK_AKPHA);
    }
}