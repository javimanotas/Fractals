using UnityEngine;

namespace Fractals
{
    /// <summary> Provides a global access point to a MonoBehaviour </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                // There is no need to asign the component to a GameObject.
                // It's created automatically if it doesn't exists
                if (_instance == null)
                {
                    var gameObject = new GameObject(typeof(T).ToString());
                    _instance = gameObject.AddComponent<T>();
                    DontDestroyOnLoad(gameObject);
                }

                return _instance;
            }
        }

        /// <summary> Initialization in case of the component is asigned to a GameObject in the hierarchy </summary>
        protected virtual void Awake() => _instance ??= this as T;
    }
}