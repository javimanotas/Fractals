using UnityEngine;

namespace Fractals
{
    /// <summary> Contains a function to directly quit de application </summary>
    public class AppQuit : MonoBehaviour
    {
        public void Quit() => Application.Quit();
    }
}