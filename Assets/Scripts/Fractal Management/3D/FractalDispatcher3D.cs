using UnityEngine;
using UnityEngine.UI;

namespace Fractals
{
    [RequireComponent(typeof(RawImage))]
    public partial class FractalDispatcher3D : FractalDispatcher
    {
        [SerializeField] ComputeShader RayComputeShader;

        protected override void AssignComputeShader() => ComputeShader = RayComputeShader;
    }
}