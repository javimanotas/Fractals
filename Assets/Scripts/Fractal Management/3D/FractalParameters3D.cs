using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher3D
    {
        [SerializeField] BulbPalette Palette;

        [SerializeField] BulbParameters Parameters;

        [SerializeField] bool Animate = false;

        [SerializeField] Camera Cam;

        [SerializeField] Light MainLight;

        protected override void InitParameters()
        {
            ComputeShader.SetVector("LightDir", MainLight.transform.forward);

            foreach (var (key, value) in Palette)
            {
                ComputeShader.SetVector(key, value);
            }

            foreach (var (key, value) in Parameters)
            {
                ComputeShader.SetFloat(key, value);
            }
        }

        protected override void Update()
        {
            ComputeShader.SetVector("CamPos", Cam.transform.position);
            ComputeShader.SetVector("Forward", Cam.transform.forward);
            ComputeShader.SetFloat("Time", Animate ? -Time.time / 7 + Mathf.PI / 2 : 0);
            AreChangesOnParameters = true;
            base.Update();
        }
    }
}