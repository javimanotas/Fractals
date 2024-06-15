namespace Fractals.UI
{
    /// <summary> Sets the number of maximum iterations of the fractal </summary>
    public class IterationsManager : ClampledIntInputField
    {
        protected override void SubmitChanges(int maxIter) => Dispatcher.MaxIter = maxIter;
    }
}