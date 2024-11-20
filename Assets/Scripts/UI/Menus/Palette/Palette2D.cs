using UnityEngine;

namespace Fractals.UI
{
    public class Palette2D : PaletteManager
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        protected override string Name => "Palette2D";

        protected override void EspecificPaletteChange(int index)
        {
            Dispatcher.PaletteIndex = index;
        }
    }
}