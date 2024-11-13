using UnityEngine;

namespace Fractals
{
    public static class Extensions
    {
        /// <summary> Changes the alpha of a color preserving the rgb values </summary>
        public static Color WithAlpha(this Color color, float a) => new(color.r, color.g, color.b, a);

        /// <summary> Returns the copy of a vector with a component modified </summary>
        public static Vector2 With(this Vector2 v, float? x = null, float? y = null) => new(x ?? v.x, y ?? v.y);

        /// <summary> Returns the copy of a vector with a component modified </summary>
        public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null) => new(x ?? v.x, y ?? v.y, z ?? v.z);

        /// <summary> Returns a vector with the same direction but with the magnitude changed to fit a range </summary>
        /// <param name="v"> Non-null vector </param>
        public static Vector3 WithMagnitudeClamped(this Vector3 v, float min, float max)
        {
            var magnitude = v.magnitude;
            return v.normalized * Mathf.Clamp(magnitude, min, max);
        }
    }
}