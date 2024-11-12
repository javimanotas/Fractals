using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Fractals
{
    /// <summary> Renders the fractal into an image after executing the compute shader </summary>
    [RequireComponent(typeof(RawImage))]
    public partial class FractalDispatcher3D : FractalDispatcher
    {
        [SerializeField] ComputeShader RayComputeShader;
        
        [SerializeField] BulbPalette Palette;

        [SerializeField] BulbParameters Parameters;

        [SerializeField] bool Animate = false;

        [SerializeField] Camera Cam;

        [SerializeField] Light MainLight;

        protected override void AssignComputeShader() => ComputeShader = RayComputeShader;

        protected override void InitParameters()
        {
        }

        void Start()
        {
            
            ComputeShader.SetVector("LightDir", MainLight.transform.forward);

            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;

            ComputeShader.SetVector("CamPos", Cam.transform.position);
            ComputeShader.SetVector("LightDir", MainLight.transform.forward);
            ComputeShader.SetFloat("Time", Animate ? -Time.time / 7 + Mathf.PI / 2 : 0);

            foreach (var (key, value) in Palette)
            {
                ComputeShader.SetVector(key, value);
            }

            foreach (var (key, value) in Parameters)
            {
                ComputeShader.SetFloat(key, value);
            }

        }
    }
}