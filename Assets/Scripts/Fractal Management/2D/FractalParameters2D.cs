using System.Linq;
using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher2D
    {
        // Needs to be an array in order to be sent to a buffer
        readonly FractalTransform2D[] _data = new FractalTransform2D[1];

        public FractalTransform2D FractalTransform
        {
            get => _data[0];
            set
            {
                AreChangesOnParameters = true;
                _data[0] = value;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || PLATFORM_STANDALONE_WIN
                Buffer.SetData(_data);
                ComputeShader.SetBuffer(0, "TransformBuffer", Buffer);
#else
                ComputeShader.SetFloat("Size", (float)_data[0].Size);
                ComputeShader.SetFloat("CenterRe", (float)_data[0].CenterRe);
                ComputeShader.SetFloat("CenterIm", (float)_data[0].CenterIm);
#endif
            }
        }

        bool _invert;

        public bool Invert
        {
            get => _invert;
            set
            {
                AreChangesOnParameters = true;
                _invert = value;
                ComputeShader.SetBool("Invert", value);
            }
        }

        int _paletteIndex;

        public int PaletteIndex
        {
            get => _paletteIndex;
            set
            {
                AreChangesOnParameters = true;
                _paletteIndex = value;
                ComputeShader.SetInt("PaletteIndex", value);
            }
        }

        int _maxIter;

        public int MaxIter
        {
            get => _maxIter;
            set
            {
                AreChangesOnParameters = true;
                _maxIter = value;
                ComputeShader.SetInt("MaxIter", value);
            }
        }

        int _antialiasingSamples;

        public int AntialiasingSamples
        {
            get => _antialiasingSamples;
            set
            {
                AreChangesOnParameters = true;
                _antialiasingSamples = value;
                ComputeShader.SetInt("AntialiasingSamples", value);
            }
        }

        public int NumCoeficients => _coeficients.Length;

        readonly float[] _coeficients = new float[10];

        public void ResetCoeficients()
        {
            for (var i = 0; i < _coeficients.Length; i++)
            {
                SetCoeficient(i, i == 1 ? 1 : 0);
            }
        }

        public float GetCoeficient(int index) => _coeficients[index];

        /// <param name="index"> degree - 1 </param>
        public void SetCoeficient(int index, float value)
        {
            AreChangesOnParameters = true;
            _coeficients[index] = value;

            var vectors = _coeficients
                .Select(x => new Vector4(x, 0, 0, 0))
                .ToArray();

            ComputeShader.SetVectorArray("Coeficients", vectors);
        }

        protected override void InitParameters()
        {
            FractalTransform = new() { Size = 2.5f, CenterRe = -0.5f };

            Invert = false;
            PaletteIndex = 0;
            MaxIter = 100;
            AntialiasingSamples = 1;
            ResetCoeficients();
        }
    }
}