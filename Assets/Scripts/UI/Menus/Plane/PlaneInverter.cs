using UnityEngine;

namespace Fractals.UI
{
    public class PlaneInverter : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        public void InvertPanel(bool invert) => Dispatcher.Invert = invert;
    }
}