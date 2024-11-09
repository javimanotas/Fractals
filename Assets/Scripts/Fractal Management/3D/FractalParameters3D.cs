using UnityEngine;

namespace Fractals
{
    public partial class FractalDispatcher3D
    {
        Camera _camera;

        public Camera Camera
        {
            get => _camera;
            set
            {
                AreChangesOnParameters = true;
                _camera = value;
                ComputeShader.SetMatrix("_CamToWorld", value.cameraToWorldMatrix);
                ComputeShader.SetMatrix("_CamInverseProj", value.projectionMatrix.inverse);
            }
        }

        Vector3 LightDir
        {
            set
            {
                AreChangesOnParameters = true;
                ComputeShader.SetVector("_LightDir", value);
            }
        }

        public Color ColA
        {
            set => ComputeShader.SetVector("_ColA", value);
        }

        public Color ColB
        {
            set => ComputeShader.SetVector("_ColB", value);
        }

        public Color BgColA
        {
            set => ComputeShader.SetVector("_BgColA", value);
        }

        public Color BgColB
        {
            set => ComputeShader.SetVector("_BgColB", value);
        }

        bool _mirror;

        public bool Mirror
        {
            get => _mirror;
            set
            {
                AreChangesOnParameters = true;
                _mirror = value;
                ComputeShader.SetBool("_Mirror", value);
            }
        }

        float _power;

        public float Power
        {
            get => _power;
            set
            {
                AreChangesOnParameters = true;
                _power = value;
                ComputeShader.SetFloat("_Power", value);
            }
        }

        float _epsilon;

        public float Epsilon
        {
            get => _epsilon;
            set
            {
                AreChangesOnParameters = true;
                _epsilon = value;
                ComputeShader.SetFloat("_Epsilon", value);
            }
        }

        Vector3 _rot;

        public Vector3 Rot
        {
            get => _rot;
            set
            {
                AreChangesOnParameters = true;
                _rot = value;
                ComputeShader.SetVector("_Rot", value);
            }
        }

        Vector3 _menger;

        public Vector3 Menger
        {
            get => _menger;
            set
            {
                AreChangesOnParameters = true;
                _menger = value;
                ComputeShader.SetVector("_Menger", value);
            }
        }

        protected override void InitParameters()
        {
            Camera = Camera.main;
            LightDir = FindFirstObjectByType<Light>().transform.forward;

            ColA = new(1, 1, 1, 1);
            ColB = new(1, 1, 1, 1);
            BgColA = new(0, 0, 0, 1);
            BgColB = new(0, 0, 0, 1);

            Mirror = false;
            Power = 9;
            Epsilon = 0.001f;
            Rot = Vector3.zero;
            Menger = Vector3.zero;
        }
    }
}