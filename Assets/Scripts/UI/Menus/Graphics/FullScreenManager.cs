using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Alternates full screen mode </summary>
    public class FullScreenManager : MonoBehaviour
    {
        [SerializeField] SwitchToggle Switch;

        public void SetFullScreen(bool fullScreen)
        {
            var res = Screen.resolutions[^1];
            var (width, height) = (res.width, res.height);
            if (fullScreen)
            {
                Screen.SetResolution(width, height, true);
            }
            else
            {
                Screen.SetResolution(2 * width / 3, 2 * height / 3, false);
            }
            PlayerPrefs.SetInt("Fullscreen", fullScreen ? 1 : 0);
        }

        public void Start() => Switch.SetOn(PlayerPrefs.GetInt("Fullscreen", 1) != 0);
    }
}