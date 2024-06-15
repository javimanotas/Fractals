using System.Linq;
using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher
    {
        /* These are the parameters sent to the compute shader.
         * Each time a parameter is changed, is sent to the GPU and _areChangesOnParameters is set to true.
         * This allows to avoid dispatching the compute shader every frame and do it only when are changes.
         * To see more detail about the parameters see the actual compute shader (Fractals2D.compute) */

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
                _buffer.SetData(_data);
                ComputeShader.SetBuffer(0, "TransformBuffer", _buffer);
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
                ComputeShader.SetBool("Julia", value);
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
                ComputeShader.SetFloat("JuliaRe", value);
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
                ComputeShader.SetFloat("JuliaIm", value);
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
                ComputeShader.SetBool("Invert", value);
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
                ComputeShader.SetInt("PaletteIndex", value);
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
                ComputeShader.SetInt("MaxIter", value);
            }
        }

        int _antialiasingSampling;

        public int AntialiasingSampling
        {
            get => _antialiasingSampling;
            set
            {
                _areChangesOnParameters = true;
                _antialiasingSampling = value;
                ComputeShader.SetInt("AntialiasingSampling", value);
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

        /// <param name="index"> degree - 1</param>
        public void SetCoeficient(int index, float value)
        {
            _areChangesOnParameters = true;
            _coeficients[index] = value;

            var vectors = _coeficients
                .Select(x => new Vector4(x, 0, 0, 0))
                .ToArray();

            ComputeShader.SetVectorArray("Coeficients", vectors);
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
            AntialiasingSampling = 1;
            ResetCoeficients();
        }
    }
}