using UnityEngine;
using UnityEngine.UI;

namespace Fractals
{
    [RequireComponent(typeof(RawImage))]
    public partial class FractalDispatcher2D : FractalDispatcher
    {
        [SerializeField] ComputeShader FloatComputeShader;

        [SerializeField] ComputeShader DoubleComputeShader;

        ComputeBuffer _buffer;

        ComputeBuffer Buffer => _buffer ??= new(1, sizeof(double) * 3); // The struct has 3 doubles

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _buffer?.Dispose(); // Garbage collector doesn't manage this, need to free manually
        }

        protected override void AssignComputeShader()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || PLATFORM_STANDALONE_WIN
            ComputeShader = DoubleComputeShader;
#else
            ComputeShader = FloatComputeShader;
#endif
        }
    }
}