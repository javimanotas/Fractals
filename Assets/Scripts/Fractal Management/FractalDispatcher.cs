using Fractals.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals
{
    /// <summary> Renders the fractal into an image after executing the compute shader </summary>
    public abstract class FractalDispatcher : MonoBehaviour
    {
        protected ComputeShader ComputeShader;

        RawImage _outputImage;

        RenderTexture _renderTex;

        readonly (int, int, int) _desiredThreadGroupSize = (8, 8, 1);

        // Is created dinamically in case of the size of the screen is not divisible by the desired size
        (int, int, int) _threadGroupSize;

        /* Each time a parameter is changed, is sent to the GPU and _areChangesOnParameters is set to true.
         * This allows to avoid dispatching the compute shader every frame and do it only when are changes.
         * To see more detail about the parameters read README.md or see the actual compute shader */
        protected bool AreChangesOnParameters = true;

        float _resolution = 1f;

        public float Resolution
        {
            set
            {
                if (_resolution != value)
                {
                    _resolution = value;
                    OnResolutionChanged();
                }
            }
        }

        bool _julia;

        public bool Julia
        {
            get => _julia;
            set
            {
                AreChangesOnParameters = true;
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
                if (Time.timeSinceLevelLoad > 0.2f)
                {
                    Julia = true;
                }

                AreChangesOnParameters = true;
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
                if (Time.timeSinceLevelLoad > 0.2f)
                {
                    Julia = true;
                }

                AreChangesOnParameters = true;
                _juliaIm = value;
                ComputeShader.SetFloat("JuliaIm", value);
            }
        }

        void Awake()
        {
            _outputImage = GetComponent<RawImage>();
            Scaler.Instance.OnResolutionChanged += OnResolutionChanged;
            AssignComputeShader();

            Julia = false;
            JuliaRe = 0;
            JuliaIm = 0;
            InitParameters();
    
            CreateRenderTexture();
        }

        protected virtual void OnDestroy() => Scaler.Instance.OnResolutionChanged -= OnResolutionChanged;

        protected abstract void AssignComputeShader();

        protected abstract void InitParameters();

        void OnResolutionChanged()
        {
            CreateRenderTexture();
            DispatchShader();
        }

        void CreateRenderTexture()
        {
            _renderTex = new(Mathf.RoundToInt(Screen.width * _resolution), Mathf.RoundToInt(Screen.height * _resolution), 0)
            {
                enableRandomWrite = true
            };

            _renderTex.Create();

            if (_outputImage == null)
            {
                _outputImage = GetComponent<RawImage>();
            }

            _outputImage.texture = _renderTex;
            ComputeShader.SetTexture(0, "FractalTex", _renderTex);
            
            _threadGroupSize = _desiredThreadGroupSize;
            _threadGroupSize.Item1 = Mathf.CeilToInt(Screen.width * _resolution / _threadGroupSize.Item1);
            _threadGroupSize.Item2 = Mathf.CeilToInt(Screen.height * _resolution / _threadGroupSize.Item2);
        }

        protected virtual void Update()
        {

            if (AreChangesOnParameters)
            {
                AreChangesOnParameters = false;
                DispatchShader();
            }
        }

        void DispatchShader() => ComputeShader.Dispatch(0, _threadGroupSize.Item1, _threadGroupSize.Item2, _threadGroupSize.Item3);
    }
}