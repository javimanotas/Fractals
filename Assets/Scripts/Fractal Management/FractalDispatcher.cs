using Fractals.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Fractals
{
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

        void Awake()
        {
            _outputImage = GetComponent<RawImage>();
            Scaler.Instance.OnResolutionChanged += OnResolutionChanged;
            AssignComputeShader();
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
            _threadGroupSize.Item1 = Mathf.CeilToInt((float)Screen.width / _threadGroupSize.Item1);
            _threadGroupSize.Item2 = Mathf.CeilToInt((float)Screen.height / _threadGroupSize.Item2);
        }
        void Update()
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