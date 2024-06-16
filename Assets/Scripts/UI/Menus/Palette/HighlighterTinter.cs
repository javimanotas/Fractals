using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
    /// <summary> Used for all the buttons, sliders... that have colors that changes with palettes <para>
    /// The UI material can't be used on them because it doesn't interact with the Image component color </para> </summary>
    [RequireComponent(typeof(Image))]
    public class HighlighterTinter : MonoBehaviour
    {
        Image _image;

        public void SetColor(Color color)
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }

            _image.color = color.WithAlpha(_image.color.a);
        }
    }
}