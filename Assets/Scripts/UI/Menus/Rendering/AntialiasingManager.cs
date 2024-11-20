namespace Fractals.UI
{
    /// <summary> Sets the number of antialiasing samples of the fractal </summary>
    public class AntialiasingManager : ClampledIntInputField
    {
        protected override void SubmitChanges(int samples) => (Dispatcher as FractalDispatcher2D).AntialiasingSamples = samples;
    }
}