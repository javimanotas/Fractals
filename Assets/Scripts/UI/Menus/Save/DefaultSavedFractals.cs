using System.Collections.Generic;

namespace Fractals
{
    /// <summary> Fractals that are saved automatically the first time the application starts </summary>
    public static class DefaultSavedFractals
    {
        static List<(string, List<string>)> fractals = new()
        {
            ("Spiral", new()
            {
                $"Inverted plane: {false}",
                $"Julia Real: {-0.929}",
                $"Julia Imaginary: {-0.8362}",
                $"Julia: {true}",
                $"Coeficient1: {-0.7}",
                $"Coeficient2: {0}",
                $"Coeficient3: {0}",
                $"Coeficient4: {0}",
                $"Coeficient5: {0}",
                $"Coeficient6: {-0.3}",
                $"Coeficient7: {0}",
                $"Coeficient8: {0}",
                $"Coeficient9: {0}",
                $"Coeficient10: {0}",
            }),
            ("Eye", new()
            {
                $"Inverted plane: {true}",
                $"Julia Real: {0}",
                $"Julia Imaginary: {0}",
                $"Julia: {false}",
                $"Coeficient1: {0}",
                $"Coeficient2: {0}",
                $"Coeficient3: {1}",
                $"Coeficient4: {0}",
                $"Coeficient5: {0}",
                $"Coeficient6: {0}",
                $"Coeficient7: {0}",
                $"Coeficient8: {0}",
                $"Coeficient9: {0}",
                $"Coeficient10: {0}",
            }),
            ("Shells", new()
            {
                $"Inverted plane: {false}",
                $"Julia Real: {0.038}",
                $"Julia Imaginary: {0.0228}",
                $"Julia: {true}",
                $"Coeficient1: {1.07}",
                $"Coeficient2: {0}",
                $"Coeficient3: {0}",
                $"Coeficient4: {0}",
                $"Coeficient5: {0}",
                $"Coeficient6: {0}",
                $"Coeficient7: {0}",
                $"Coeficient8: {0}",
                $"Coeficient9: {-1}",
                $"Coeficient10: {0}",
            }),
        };

        public static void Save(FractalSaver saver)
        {
            foreach (var (name, lines) in fractals)
            {
                saver.Save(name, lines);
            }
        }
    }
}