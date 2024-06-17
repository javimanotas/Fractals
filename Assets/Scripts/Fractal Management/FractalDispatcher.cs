using UnityEngine;
using UnityEngine.UI;
using Fractals.UI;

namespace Fractals
{
    /// <summary> Renders the fractal into an image after executing the compute shader </summary>
    [RequireComponent(typeof(RawImage))]
    public partial class FractalDispatcher : MonoBehaviour
    {
        [SerializeField] ComputeShader FloatComputeShader;

        [SerializeField] ComputeShader DoubleComputeShader;

        ComputeShader _computeShader;

        RawImage _outputImage;

        RenderTexture _renderTex;

        readonly (int, int, int) _desiredThreadGroupSize = (8, 8, 1);
        
        // Is created dinamically in case of the size of the screen is not divisible by the desired size
        (int, int, int) _threadGroupSize;

        ComputeBuffer _buffer;

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

        void OnDestroy() => _buffer?.Dispose(); // Garbage collector doesn't manage this, need to free manually

        void Awake()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || PLATFORM_STANDALONE_WIN
            _buffer = new(1, sizeof(double) * 3); // The struct has 3 doubles
            _computeShader = DoubleComputeShader;
#else
            _computeShader = FloatComputeShader;
#endif
            _outputImage = GetComponent<RawImage>();
            
            Scaler.Instance.OnResolutionChanged += OnResolutionChanged;
            
            InitParameters();
            CreateRenderTexture();
        }

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
            _computeShader.SetTexture(0, "FractalTex", _renderTex);

            _threadGroupSize = _desiredThreadGroupSize;
            _threadGroupSize.Item1 = Mathf.CeilToInt((float)Screen.width / _threadGroupSize.Item1);
            _threadGroupSize.Item2 = Mathf.CeilToInt((float)Screen.height / _threadGroupSize.Item2);
        }

        void Update()
        {
            if (_areChangesOnParameters)
            {
                _areChangesOnParameters = false;
                DispatchShader();
            }
        }

        void DispatchShader() => _computeShader.Dispatch(0, _threadGroupSize.Item1, _threadGroupSize.Item2, _threadGroupSize.Item3);
    }
}