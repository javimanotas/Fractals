using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fractals
{
    /// <summary> Transforms the zoom and offset of the fractal </summary>
    public class FractalTransformer2D : MonoBehaviour, IDragHandler
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        const float _ZOOM_SPEED = 1.05f;

        public void OnDrag(PointerEventData eventData)
        {
            var trans = Dispatcher.FractalTransform;

            var speed = -trans.Size / Screen.height;
            
            trans.CenterRe += eventData.delta.x * speed;
            trans.CenterIm += eventData.delta.y * speed;

            Dispatcher.FractalTransform = trans;
        }

        void Update()
        {
            static double Lerp(double a, double b, double t) => a + (b - a) * t;
            
            var scroll = Input.mouseScrollDelta.y;

            if (scroll == 0)
            {
                return;
            }
            
            var trans = Dispatcher.FractalTransform;
            var zoom = Math.Pow(_ZOOM_SPEED, scroll);
            var mouseRelative = Input.mousePosition / Screen.height;
            var screenRatio = (double)Screen.width / Screen.height;

            trans.Size /= zoom;

            var mouse0 = (mouseRelative.x - 0.5 * screenRatio) * trans.Size + trans.CenterRe;
            var mouse1 = (mouseRelative.y - 0.5) * trans.Size + trans.CenterIm;

            // Zoom also changes offset to "zoom in the cursor position"
            trans.CenterRe = Lerp(mouse0, trans.CenterRe, 2 - zoom);
            trans.CenterIm = Lerp(mouse1, trans.CenterIm, 2 - zoom);

            Dispatcher.FractalTransform = trans;
        }
    }
}