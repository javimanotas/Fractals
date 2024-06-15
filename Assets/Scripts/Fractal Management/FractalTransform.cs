namespace Fractals
{
    /// <summary> Data sent to the compute shader in a buffer (only the data that requires double precision) </summary>
    [System.Serializable]
    public struct FractalTransform
    {
        /// <summary> Height in complex plane units of the screen </summary>
        public double Size;
        
        /// <summary> Coordinates of the complex plane of the center of the screen </summary>
        public double CenterRe, CenterIm;
    }
}