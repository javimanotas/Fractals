using UnityEngine;
using UnityEngine.UI;

namespace Fractals.UI
{
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