using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher3D
    {
        [SerializeField] BulbPalette _palette;

        public BulbPalette Palette
        {
            private get => _palette;
            set
            {
                _palette = value;

                foreach (var (key, v) in Palette)
                {
                    ComputeShader.SetVector(key, v);
                }
            }
        }

        [SerializeField] BulbParameters Parameters;

        [SerializeField] Camera Cam;

        [SerializeField] Light MainLight;

        protected override void InitParameters()
        {
            ComputeShader.SetVector("LightDir", MainLight.transform.forward);

            Palette = Palette;

            foreach (var (key, value) in Parameters)
            {
                ComputeShader.SetFloat(key, value);
            }
        }

        protected override void Update()
        {
            ComputeShader.SetVector("CamPos", Cam.transform.position);
            ComputeShader.SetVector("Forward", Cam.transform.forward);
            ComputeShader.SetFloat("Time", -Time.time / 7 + Mathf.PI / 2);
            AreChangesOnParameters = true;
            base.Update();
        }
    }
}