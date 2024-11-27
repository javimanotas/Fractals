using UnityEngine;
using System.ComponentModel;

namespace Fractals.UI
{
    [System.Serializable]
    enum Property
    {
        X, Y, Zoom
    }

    public class TransformSetter : ClampledFloatInputField
    {
        [SerializeField] Property Property;

        protected override void SubmitChanges(float f)
        {
            var dispatcher = Dispatcher as FractalDispatcher2D;
            var transform = dispatcher.FractalTransform;

            switch (Property)
            {
                case Property.X: transform.CenterRe = f; break;
                case Property.Y: transform.CenterIm = f; break;
                case Property.Zoom: transform.Size = f; break;
                default: throw new InvalidEnumArgumentException();
            }

            dispatcher.FractalTransform = transform;
        }
    }
}