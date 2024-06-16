using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Interfaces with the UI system to invert the complex plane </summary>
    public class PlaneInverter : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        public void InvertPanel(bool invert) => Dispatcher.Invert = invert;
    }
}