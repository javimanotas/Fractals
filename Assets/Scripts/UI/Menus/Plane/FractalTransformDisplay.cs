using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Displays the center and size of the fractal </summary>
    public class FractalTransformDisplay : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        [SerializeField] TextMeshProUGUI XCoordinate;

        [SerializeField] TextMeshProUGUI YCoordinate;

        [SerializeField] TextMeshProUGUI Size;

        public void Update()
        {
            static string Format(double d) => d.ToString();

            var transform = Dispatcher.FractalTransform;

            XCoordinate.text = Format(transform.CenterRe);
            YCoordinate.text = Format(transform.CenterIm);
            Size.text = Format(transform.Size);
        }
    }
}