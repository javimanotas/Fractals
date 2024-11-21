using System;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Notifies all the listeners changes of the resolution </summary>
    public class Scaler : Singleton<Scaler>
    {
        public event Action OnResolutionChanged;

        (int, int) _pixels;

        void Start() => _pixels = (Screen.width, Screen.height);

        void Update()
        {
            if (_pixels != (Screen.width, Screen.height))
            {
                OnResolutionChanged?.Invoke();
            }

            _pixels = (Screen.width, Screen.height);
        }
    }
}