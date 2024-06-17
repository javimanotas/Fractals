using System.Linq;
using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher
    {
        /* These are the parameters sent to the compute shader.
         * Each time a parameter is changed, is sent to the GPU and _areChangesOnParameters is set to true.
         * This allows to avoid dispatching the compute shader every frame and do it only when are changes.
         * To see more detail about the parameters read README.md or see the actual compute shader (Fractals2D.compute) */

        bool _areChangesOnParameters = true;

        // Needs to be an array in order to be sent to a buffer
        readonly FractalTransform[] _data = new FractalTransform[1];

        public FractalTransform FractalTransform
        {
            get => _data[0];
            set
            {
                _areChangesOnParameters = true;
                _data[0] = value;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || PLATFORM_STANDALONE_WIN
                _buffer.SetData(_data);
                _computeShader.SetBuffer(0, "TransformBuffer", _buffer);
#else
                _computeShader.SetFloat("Size", (float)_data[0].Size);
                _computeShader.SetFloat("CenterRe", (float)_data[0].CenterRe);
                _computeShader.SetFloat("CenterIm", (float)_data[0].CenterIm);
#endif
            }
        }

        bool _julia;

        public bool Julia
        {
            get => _julia;
            set
            {
                _areChangesOnParameters = true;
                _julia = value;
                _computeShader.SetBool("Julia", value);
            }
        }

        float _juliaRe;

        public float JuliaRe
        {
            get => _juliaRe;
            set
            {
                _areChangesOnParameters = true;
                _juliaRe = value;
                _computeShader.SetFloat("JuliaRe", value);
            }
        }

        float _juliaIm;

        public float JuliaIm
        {
            get => _juliaIm;
            set
            {
                _areChangesOnParameters = true;
                _juliaIm = value;
                _computeShader.SetFloat("JuliaIm", value);
            }
        }

        bool _invert;

        public bool Invert
        {
            get => _invert;
            set
            {
                _areChangesOnParameters = true;
                _invert = value;
                _computeShader.SetBool("Invert", value);
            }
        }

        int _paletteIndex;

        public int PaletteIndex
        {
            get => _paletteIndex;
            set
            {
                _areChangesOnParameters = true;
                _paletteIndex = value;
                _computeShader.SetInt("PaletteIndex", value);
            }
        }

        int _maxIter;

        public int MaxIter
        {
            get => _maxIter;
            set
            {
                _areChangesOnParameters = true;
                _maxIter = value;
                _computeShader.SetInt("MaxIter", value);
            }
        }

        int _antialiasingSamples;

        public int AntialiasingSamples
        {
            get => _antialiasingSamples;
            set
            {
                _areChangesOnParameters = true;
                _antialiasingSamples = value;
                _computeShader.SetInt("AntialiasingSamples", value);
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
            _areChangesOnParameters = true;
            _coeficients[index] = value;

            var vectors = _coeficients
                .Select(x => new Vector4(x, 0, 0, 0))
                .ToArray();

            _computeShader.SetVectorArray("Coeficients", vectors);
        }

        void InitParameters()
        {
            FractalTransform = new() { Size = 2.5f, CenterRe = -0.5f };
            Julia = false;
            JuliaRe = 0;
            JuliaIm = 0;
            Invert = false;
            PaletteIndex = 0;
            MaxIter = 100;
            AntialiasingSamples = 1;
            ResetCoeficients();
        }
    }
}