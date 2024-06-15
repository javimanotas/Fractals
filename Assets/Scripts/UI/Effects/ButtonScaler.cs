using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Scaling animatio for a button </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ButtonScaler : ButtonAnimation
    {
        RectTransform _childRect;

        const float _SCALE = 1.3f;

        Vector2 _defaultScale;

        void Start()
        {
            var image = Instantiate(new GameObject("child image"), GetComponent<RectTransform>())
                .AddComponent<Image>();
            
            image.raycastTarget = false;
            var im = GetComponent<Image>();
            image.sprite = im.sprite;
            image.material = im.material;
            image.SetNativeSize();
            _childRect = image.GetComponent<RectTransform>();
            _defaultScale = _childRect.localScale;
        }

        Action<float> LerpScale(float minScale, float maxScale) => f
            => _childRect.localScale = _defaultScale * Mathf.Lerp(minScale, maxScale, f);

        protected override Action<float> HoverAnimation => LerpScale(1, _SCALE);

        protected override Action<float> ClickAnimation => f => { };
    }
}