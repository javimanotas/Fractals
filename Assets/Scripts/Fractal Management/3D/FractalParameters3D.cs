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
                AreChangesOnParameters = true;

                foreach (var (key, v) in Palette)
                {
                    ComputeShader.SetVector(key, v);
                }
            }
        }

        [SerializeField] BulbParameters Parameters;

        [SerializeField] Camera Cam;

        (Vector3, Vector3)? _camTransform = null;

        (Vector3, Vector3) CamTransform => (Cam.transform.position, Cam.transform.forward);

        [SerializeField] Light MainLight;

        float _time;

        public float Time
        {
            get => _time;
            set
            {
                _time = value;
                ComputeShader.SetFloat("Time", _time);
                AreChangesOnParameters = true;
            }
        }

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
            if (_camTransform is null || _camTransform != CamTransform)
            {
                _camTransform = CamTransform;
                ComputeShader.SetVector("CamPos", _camTransform.Value.Item1);
                ComputeShader.SetVector("Forward", _camTransform.Value.Item2);
                AreChangesOnParameters = true;
            }
            
            base.Update();
        }
    }
}