using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace Fractals
{
    /// <summary> Color palettes for the mandelbulb fractal </summary>
    [CreateAssetMenu(fileName = "Bulb palette ", menuName = "Bulb palette")]
    public class BulbPalette : ScriptableObject, IEnumerable<(string, Color)>
    {
        [SerializeField] Color BgColor = new(0.149f, 0.3617f, 1);

        [SerializeField] Color HaloColor = new(0.5f, 0.75f, 1);

        [SerializeField] Color BulbColor = new(0, 0.666f, 1);
        
        [SerializeField] Color Tint = new(1, 0.824f, 0.78f);

        /* All the properties are included automatically to the enumerator so
        be carefull and don't add porperties with type different than Color */
        public IEnumerator<(string, Color)> GetEnumerator() => GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(field => field.FieldType == typeof(Color))
            .Select(field => (field.Name, (Color)field.GetValue(this)))
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool operator ==(BulbPalette left, BulbPalette right) => left.GetEnumerator() == right.GetEnumerator();

        public static bool operator !=(BulbPalette left, BulbPalette right) => !(left == right);

        public override bool Equals(object other) => other is BulbPalette pallette && pallette == this;

        public override int GetHashCode() => GetEnumerator().GetHashCode();
    }
}