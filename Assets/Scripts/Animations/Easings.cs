using System;
using UnityEngine;

namespace Fractals.Animations
{
    /// <summary> Usefull functions for lerping between values </summary>
    public static class Easings
    {
        /// <summary> Starts slow and ends slow </summary>
        public static float SmoothStep(float x) => 3 * x * x - 2 * x * x * x;

        /// <summary> Goes from 0 to 1 and goes back to 0 </summary>
        public static float InOutParabola(float x) => -4 * x * x + 4 * x;

        /// <summary> Exceeds the limit and goes smoothly back to 1 </summary>
        /// <param name="amplitude"> For values >= 1, how muchs exceeds the limit </param>
        public static Func<float, float> OutBack(float amplitude) => x
            => 1 + (amplitude + 1) * Mathf.Pow(x - 1, 3) + amplitude * Mathf.Pow(x - 1, 2);
    }
}