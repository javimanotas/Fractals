using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fractals
{
    /// <summary> Contains a function to directly load a new scene </summary>
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}