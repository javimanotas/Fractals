﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Fractals
{
    public class FractalSaver
    {
        readonly FractalDispatcher _dispatcher;

        public static string SavePath => Application.persistentDataPath;

        public static string FileExtension => ".fractal2d";

        public FractalSaver(FractalDispatcher dispatcher) => _dispatcher = dispatcher;

        public void Save(string fileName)
        {
            var lines = new List<string>
            {
                $"Inverted plane: {_dispatcher.Invert}",
                $"Julia: {_dispatcher.Julia}",
                $"Julia Real: {_dispatcher.JuliaRe}",
                $"Julia Imaginary: {_dispatcher.JuliaIm}",
            };

            for (var i = 0; i < _dispatcher.NumCoeficients; i++)
            {
                lines.Add($"Coeficient{i + 1}: {_dispatcher.GetCoeficient(i)}");
            }

            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }

            File.WriteAllLines(Path.Combine(SavePath, $"{fileName}.fractal2d"), lines);
        }
    }
}