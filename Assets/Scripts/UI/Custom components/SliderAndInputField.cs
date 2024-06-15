using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fractals.UI
{
    public class SliderAndInputField : MonoBehaviour
    {
        [SerializeField] UnityEvent<float> OnValueChanged;

        [SerializeField] bool HasDefaultValue = false;

        [SerializeField] float DefaultValue;

        float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                _inputField.text = value.ToString();
                _slider.value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        Slider _slider;

        TMP_InputField _inputField;

        void Start()
        {
            _slider = GetComponentInChildren<Slider>();
            _inputField = GetComponentInChildren<TMP_InputField>();

            _slider.onValueChanged.AddListener(f => Value = f);

            _inputField.onEndEdit.AddListener(f =>
            {
                try { Value = Mathf.Clamp(float.Parse(f), _slider.minValue, _slider.maxValue); }
                catch { }
            });

            if (HasDefaultValue)
            {
                Value = DefaultValue;
            }
        }
    }
}