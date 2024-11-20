using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Sets the number of fps of the whole application </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FpsManager : MonoBehaviour
    {
        TextMeshProUGUI _fpsText;

        readonly int[] _avaliableFps = { 30, 60, 120, 144, 240 };

        int _fractalFpsIndex;

        void Start()
        {
            _fpsText = GetComponent<TextMeshProUGUI>();
            QualitySettings.vSyncCount = 0;
            _fractalFpsIndex = PlayerPrefs.GetInt("FPS", 1);
            ChangeFps(0);
        }

        public void ChangeFps(int next)
        {
            _fractalFpsIndex += next;
            _fractalFpsIndex += _avaliableFps.Length;
            _fractalFpsIndex %= _avaliableFps.Length;

            var fps = _avaliableFps[_fractalFpsIndex];
            Application.targetFrameRate = fps;
            _fpsText.text = fps.ToString();
            PlayerPrefs.SetInt("FPS", _fractalFpsIndex);
        }
    }
}