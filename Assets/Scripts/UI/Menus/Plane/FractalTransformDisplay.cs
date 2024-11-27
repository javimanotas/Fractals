using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Displays the center and size of the fractal </summary>
    public class FractalTransformDisplay : MonoBehaviour
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        [SerializeField] TMP_InputField XCoordinate;

        [SerializeField] TMP_InputField YCoordinate;

        [SerializeField] TMP_InputField Size;

        void Start() => Dispatcher.OnChangeTransform += UpdateText;

        void UpdateText()
        {
            static string Format(double d) => d.ToString();

            var transform = Dispatcher.FractalTransform;

            XCoordinate.text = Format(transform.CenterRe);
            YCoordinate.text = Format(transform.CenterIm);
            Size.text = Format(transform.Size);
        }
    }
}