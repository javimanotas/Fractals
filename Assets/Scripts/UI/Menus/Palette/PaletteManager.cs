using System.Collections.Generic;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Changes between palettes both in UI and in the fractal </summary>
    public class PaletteManager : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        [SerializeField] Material Mat;

        [SerializeField] RadioButton[] RadioButtons;

        IEnumerable<HighlighterTinter> _highlighters;

        void Start() => RadioButtons[PlayerPrefs.GetInt("Palette", 0)].OnPointerClick(null);

        public void ChangePalette(int index)
        {
            Dispatcher.PaletteIndex = index;

            var (darkColor, lightColor, highlightColor) = index switch
            {
                0 => (
                    new Color(0.0619f, 0.0619f, 0.0999f),
                    new Color(0.4666f, 0.764f, 0.9960f),
                    new Color(0.1568f, 0.2588f, 0.3490f)
                ),
                _ => (
                    new Color(0.0980f, 0.0627f, 0.0861f),
                    new Color(1, 0.5123f, 0.468f),
                    new Color(0.3490f, 0.1568f, 0.1607f)
                )
            };

            foreach (var highlighter in _highlighters ??= FindObjectsOfType<HighlighterTinter>())
            {
                highlighter.SetColor(highlightColor);
            }

            Mat.SetColor("_DarkColor", darkColor);
            Mat.SetColor("_LightColor", lightColor);

            PlayerPrefs.SetInt("Palette", index);
        }
    }
}