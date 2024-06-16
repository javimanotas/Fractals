using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fractals
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}