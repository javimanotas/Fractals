using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Modifies Julia set parameters </summary>
    public class JuliaManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        [SerializeField] SwitchToggle Switch;

        public void SetRealJulia(float f)
        {
            Dispatcher.JuliaRe = f;
            TurnSwitch();
        }

        public void SetImaginaryJulia(float f)
        {
            Dispatcher.JuliaIm = f;
            TurnSwitch();
        }

        void TurnSwitch()
        {
            /* When a scene loads it automatically sets the values to all the
            UI components including the Julia ones. That produces the Julia
            switch to turn on so adding this condidtion check fixes that bug.
            I know it's an horrible solution but a better way of doing it will
            required too many changes on the UI system  :( */
            if (Time.timeSinceLevelLoad > 0.3f && !FractalLoader.IsAnyoneLoading)
            {
                Switch.SetOn(true);
            }
        }

        public void OnToggle() => Dispatcher.Julia = Switch.IsTurnedOn;
    }
}