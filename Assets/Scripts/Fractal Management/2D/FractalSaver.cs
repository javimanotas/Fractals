using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Fractals
{
    /// <summary> Saves the current fractal configuration into a file </summary>
    public class FractalSaver
    {
        readonly FractalDispatcher2D _dispatcher;

        public static string SavePath => Application.persistentDataPath;

        public static string FileExtension => ".fractal2d";

        public FractalSaver(FractalDispatcher2D dispatcher) => _dispatcher = dispatcher;

        public void Save(string fileName)
        {
            var lines = new List<string>
            {
                $"Inverted plane: {_dispatcher.Invert}",
                $"Julia Real: {_dispatcher.JuliaRe}",
                $"Julia Imaginary: {_dispatcher.JuliaIm}",
                $"Julia: {_dispatcher.Julia}",
            };

            for (var i = 0; i < _dispatcher.NumCoeficients; i++)
            {
                lines.Add($"Coeficient{i + 1}: {_dispatcher.GetCoeficient(i)}");
            }

            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }

            File.WriteAllLines(Path.Combine(SavePath, $"{fileName}{FileExtension}"), lines);
        }
    }
}