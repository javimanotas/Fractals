using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fractals
{
    /// <summary> Loads directly a new scene </summary>
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}