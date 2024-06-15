using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Selects the of the polinomial to change its coeficient later </summary>
    public class DegreeSelector : ClampledIntInputField
    {
        [SerializeField] PolinomialManager Polinomial;

        protected override void SubmitChanges(int n) => Polinomial.SelectedDegree = n;
    }
}