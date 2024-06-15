namespace Fractals.UI
{
    /// <summary> Sets the resolution of the fractal image </summary>
    public class ResolutionManager : ClampledIntInputField
    {
        protected override void SubmitChanges(int resolution) => Dispatcher.Resolution = resolution / 100f;
    }
}