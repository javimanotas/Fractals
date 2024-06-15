using System.Linq;
using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Custom implementation of a input field <para>
    /// If the value set is not on the desired interval, it will be clamped</para> </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public abstract class ClampledIntInputField : MonoBehaviour
    {
        [SerializeField] protected FractalDispatcher Dispatcher;

        [SerializeField] int MinValue, MaxValue, Default;

        [SerializeField] string SaveName;

        bool HasPersistentValue => !string.IsNullOrEmpty(SaveName);

        int _lastCorrectValue;

        TMP_InputField _inputField;

        void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onEndEdit.AddListener(OnEndEdit);

            var text = GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "Placeholder");

            _lastCorrectValue = HasPersistentValue ? PlayerPrefs.GetInt(SaveName, Default) : Default;
            _inputField.text = _lastCorrectValue.ToString();
            OnEndEdit("");
        }

        public void SetValue(int value)
        {
            _inputField.text = value.ToString();
            OnEndEdit("");
        }

        protected abstract void SubmitChanges(int n);

        public void OnEndEdit(string _) // To match the signature of the unity event
        {
            int num;

            try
            {
                num = int.Parse(_inputField.text);
                num = Mathf.Clamp(num, MinValue, MaxValue);
                _lastCorrectValue = num;
            }
            catch
            {
                num = _lastCorrectValue;
            }

            if (HasPersistentValue)
            {
                PlayerPrefs.SetInt(SaveName, num);
            }

            _inputField.text = num.ToString();
            SubmitChanges(num);
        }
    }
}