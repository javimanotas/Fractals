using System;
using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Interfaces with the UI system and the FractalSaver class </summary>
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        [SerializeField] TMP_InputField InputField;

        FractalSaver _saver;

        public event Action OnFractalSave;

        void Awake()
        {
            _saver = new(Dispatcher);

            if (PlayerPrefs.GetInt("First time running application", 1) != 0)
            {
                DefaultSavedFractals.Save(_saver);
                PlayerPrefs.SetInt("First time running application", 0);
            }
        }

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