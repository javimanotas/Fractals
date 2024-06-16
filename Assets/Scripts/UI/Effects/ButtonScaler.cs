using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Scaling animation for a button </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ButtonScaler : ButtonAnimation
    {
        RectTransform _childRect;

        const float _SCALE = 1.3f;

        Vector2 _defaultScale;

        void Start()
        {
            // Creates a new image to be scalled instead of the current one so scaling doesn't conflict with the raycast
            var image = new GameObject("child image")
                .AddComponent<Image>();
            image.transform.SetParent(GetComponent<RectTransform>());

            var rect = image.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.localScale = Vector2.one;

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