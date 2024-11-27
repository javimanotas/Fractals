using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fractals
{
    /// <summary> Transforms the zoom and offset of the fractal </summary>
    public class FractalTransformer2D : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] FractalDispatcher2D Dispatcher;

        [SerializeField] Image ZoomImage; // Area zoom image

        RectTransform _zoomImageRect;

        void Start() => _zoomImageRect = ZoomImage.GetComponent<RectTransform>();

        const float _ZOOM_SPEED = 1.05f;

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                var trans = Dispatcher.FractalTransform;

                var speed = -trans.Size / Screen.height;
            
                trans.CenterRe += eventData.delta.x * speed;
                trans.CenterIm += eventData.delta.y * speed;

                Dispatcher.FractalTransform = trans;
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ZoomImage.enabled = true;
                _zoomImageRect.anchoredPosition = eventData.pressPosition;
                _zoomImageRect.localScale = (eventData.position - eventData.pressPosition) / 100;
            }
        }

        (double, double) ScreenToFractalCoords(Vector2 screenCoords)
        {
            var trans = Dispatcher.FractalTransform;

            var offsetX = screenCoords.x - Screen.width / 2.0;
            var offsetY = screenCoords.y - Screen.height / 2.0;

            offsetX *= trans.Size / Screen.height;
            offsetY *= trans.Size / Screen.height;

            return (offsetX + trans.CenterRe, offsetY + trans.CenterIm);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                ZoomImage.enabled = false;

                var trans = Dispatcher.FractalTransform;
                var (pressedPosRe, pressedPosIm) = ScreenToFractalCoords(eventData.pressPosition);
                var (releasePosRe, releasePosIm) = ScreenToFractalCoords(eventData.position);

                trans.CenterRe = (pressedPosRe + releasePosRe) / 2.0;
                trans.CenterIm = (pressedPosIm + releasePosIm) / 2.0;
                trans.Size = Math.Abs(pressedPosIm - releasePosIm);
                Dispatcher.FractalTransform = trans;
            }
        }

        public void OnPointerDown(PointerEventData eventData) { }

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