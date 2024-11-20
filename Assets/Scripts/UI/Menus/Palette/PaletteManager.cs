using System.Collections.Generic;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Changes between palettes both in UI and in the fractal </summary>
    public abstract class PaletteManager : MonoBehaviour
    {
        [SerializeField] Material Mat;

        [SerializeField] RadioButton[] RadioButtons;

        IEnumerable<HighlighterTinter> _highlighters;

        protected abstract string Name { get; }

        void Start() => RadioButtons[PlayerPrefs.GetInt(Name, 0)].OnPointerClick(null);

        protected abstract void EspecificPaletteChange(int index);

        (Color, Color, Color) Darks => (
            new(0.0619f, 0.0619f, 0.0999f),
            new(0.4666f, 0.764f, 0.9960f),
            new(0.1568f, 0.2588f, 0.3490f)
        );

        (Color, Color, Color) Lights => (
            new(0.0980f, 0.0627f, 0.0861f),
            new(1, 0.5123f, 0.468f),
            new(0.3490f, 0.1568f, 0.1607f)
        );

        (Color, Color, Color) Highlights => (
            new(0.0688f, 0.0980f, 0.0627f),
            new(0.6601f, 1, 0.4666f),
            new(0.2311f, 0.3490f, 0.1568f)
        );

        public void ChangePalette(int index)
        {
            EspecificPaletteChange(index);
            
            var (darkColor, lightColor, highlightColor) = index switch
            {
                0 => Darks,
                1 => Lights,
                _ => Highlights,
            };

            foreach (var highlighter in _highlighters ??= FindObjectsByType<HighlighterTinter>(FindObjectsSortMode.None))
            {
                highlighter.SetColor(highlightColor);
            }

            Mat.SetColor("_DarkColor", darkColor);
            Mat.SetColor("_LightColor", lightColor);

            PlayerPrefs.SetInt("Palette", index);
        }
    }
}