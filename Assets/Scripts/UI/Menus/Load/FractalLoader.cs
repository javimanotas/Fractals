using UnityEngine;
using System;
using System.Collections.Generic;

namespace Fractals.UI
{
    /// <summary> Loads a saved fractal given the lines of the file </summary>
    public class FractalLoader : MonoBehaviour
    {
        [SerializeField] SwitchToggle InvertedSwitch;

        [SerializeField] SwitchToggle JuliaSwitch;

        [SerializeField] SliderAndInputField ReJuliaSlider;

        [SerializeField] SliderAndInputField ImJuliaSlider;

        [SerializeField] PolinomialManager PolinomialManager;

        public void Load(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var tokens = line.Split(':');
                var key = tokens[0];
                var value = tokens[1][1..];

                var selectedDegree = PolinomialManager.SelectedDegree;

                ((Action)(key switch
                {
                    "Inverted plane" => () => InvertedSwitch.SetOn(bool.Parse(value)),
                    "Julia" => () => JuliaSwitch.SetOn(bool.Parse(value)),
                    "Julia Real" => () => ReJuliaSlider.Value = float.Parse(value),
                    "Julia Imaginary" => () => ImJuliaSlider.Value = float.Parse(value),
                    var str => () =>
                    {
                        var degree = int.Parse(str["Coeficient".Length..].ToString());
                        PolinomialManager.SelectedDegree = degree;
                        PolinomialManager.SetCoeficient(float.Parse(value));
                    }
                })).Invoke();

                PolinomialManager.SelectedDegree = selectedDegree;
            }
        }
    }
}