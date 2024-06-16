using System;
using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Interfaces with the UI system and the FractalSaver class </summary>
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        [SerializeField] TMP_InputField InputField;

        FractalSaver _saver;

        public event Action OnFractalSave;

        void Start() => _saver = new(Dispatcher);

        public void SaveCurrentFractalParameters()
        {
            if (!string.IsNullOrEmpty(InputField.text))
            {
                _saver.Save(InputField.text);
            }

            InputField.text = "";
            OnFractalSave?.Invoke();
        }
    }
}