using UnityEngine;

namespace Fractals
{
    /// <summary> Opens the remote repository on the default browser </summary>
    public class Help : MonoBehaviour
    {
        public void GetHelp() => Application.OpenURL("https://github.com/javimanotas/Fractals");
    }
}