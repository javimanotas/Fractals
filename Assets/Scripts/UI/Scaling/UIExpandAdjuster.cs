using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Avoids the menu to appear on screen when it's hided and the resolution is very wide </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIExpandAdjuster : MonoBehaviour
    {
        RectTransform _rect;

        void Start()
        {
            _rect = GetComponent<RectTransform>();
            AdjustOffset();
            Scaler.Instance.OnResolutionChanged += AdjustOffset;
        }

        void AdjustOffset()
        {
            var ratio = (float)Screen.width / Screen.height;
            
            if (ratio < 16f / 9)
            {
                //_rect.anchoredPosition = Vector2.up * (Screen.height * (16f / 9 - ratio));
            }
            else
            {
                _rect.anchoredPosition = Vector2.zero;
            }
        }
    }
}