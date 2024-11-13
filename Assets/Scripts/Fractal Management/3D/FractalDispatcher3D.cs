using UnityEngine;
using UnityEngine.UI;

namespace Fractals
{
    /// <summary> Renders the fractal into an image after executing the compute shader </summary>
    [RequireComponent(typeof(RawImage))]
    public partial class FractalDispatcher3D : FractalDispatcher
    {
        [SerializeField] ComputeShader RayComputeShader;

        protected override void AssignComputeShader() => ComputeShader = RayComputeShader;
    }
}