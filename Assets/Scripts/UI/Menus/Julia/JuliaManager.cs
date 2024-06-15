using UnityEngine;

namespace Fractals.UI
{
    public class JuliaManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        [SerializeField] SwitchToggle Switch;

        public void SetRealJulia(float f) => Dispatcher.JuliaRe = f;

        public void SetImaginaryJulia(float f) => Dispatcher.JuliaIm = f;

        public void OnToggle() => Dispatcher.Julia = Switch.IsTurnedOn;
    }
}