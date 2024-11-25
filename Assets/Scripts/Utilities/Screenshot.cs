using System;
using System.IO;
using UnityEngine;

namespace Fractals
{
    /// <summary> Saves the fractal as a png </summary>
    public class Screenshot : MonoBehaviour
    {
        [SerializeField] FractalDispatcher Dispatcher;

        public void Capture()
        {
            var bytes = Dispatcher.RenderTextureBytes;

            var directory = Path.Combine(Application.dataPath, "Screenshots");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fullPath = Path.Combine(Application.dataPath, "Screenshots", $"Fractal_{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png");
            File.WriteAllBytes(fullPath, bytes);
        }
    }
}