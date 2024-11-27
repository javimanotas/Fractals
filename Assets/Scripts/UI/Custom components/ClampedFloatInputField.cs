using System.Linq;
using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Same as ClampedIntInputField but for floats </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public abstract class ClampledFloatInputField : MonoBehaviour
    {
        [SerializeField] protected FractalDispatcher Dispatcher;

        [SerializeField] float MinValue, MaxValue, Default;

        float _lastCorrectValue;

        protected TMP_InputField InputField;

        void Start()
        {
            InputField = GetComponent<TMP_InputField>();
            InputField.onEndEdit.AddListener(OnEndEdit);

            var text = GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "Placeholder");

            _lastCorrectValue = Default;
            InputField.text = _lastCorrectValue.ToString();
            OnEndEdit(null);
        }

        protected abstract void SubmitChanges(float f);

        public void OnEndEdit(string _)
        {
            float num;

            try
            {
                num = float.Parse(InputField.text);
                num = Mathf.Clamp(num, MinValue, MaxValue);
                _lastCorrectValue = num;
            }
            catch
            {
                num = _lastCorrectValue;
            }

            InputField.text = num.ToString();
            SubmitChanges(num);
        }
    }
}