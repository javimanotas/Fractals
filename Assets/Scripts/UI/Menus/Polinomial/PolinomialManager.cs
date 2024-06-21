using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Modifies the polinomial coeficients of the fractal </summary>
    public class PolinomialManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        [SerializeField] TMP_InputField DegreeInputField;

        [SerializeField] SliderAndInputField Coeficient;

        int _selectedDegree;

        public int SelectedDegree
        {
            get => _selectedDegree;
            set
            {
                DegreeInputField.text = value.ToString();
                _selectedDegree = value;
                Coeficient.Value = Dispatcher.GetCoeficient(_selectedDegree - 1);
            }
        }

        void Start() => SelectedDegree = 2;

        /// <summary> Sets the coeficients of the selected degree </summary>
        public void SetCoeficient(float coeficient) => Dispatcher.SetCoeficient(_selectedDegree - 1, coeficient);

        public void Reset()
        {
            Dispatcher.ResetCoeficients();
            SelectedDegree = 2;
        }
    }
}