using UnityEngine;
using UnityEngine.EventSystems;

namespace Fractals
{
    [RequireComponent(typeof(Camera))]
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] FractalDispatcher3D Dispatcher;

        Camera _cam;

        const float _SPEED = 5;

        Vector3 Forward => transform.forward.With(y: 0).normalized;
        
        Vector3 Up => Vector3.up;

        Vector3 Right => transform.right.With(y: 0).normalized;

        void Start() => _cam = GetComponent<Camera>();

        void Update()
        {
            Vector3 input = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) input += Forward;
            if (Input.GetKey(KeyCode.S)) input -= Forward;
            if (Input.GetKey(KeyCode.D)) input += Right;
            if (Input.GetKey(KeyCode.A)) input -= Right;
            if (Input.GetKey(KeyCode.LeftShift)) input += Up;
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftCommand)) input -= Up;

            if (input != Vector3.zero)
            {
                transform.position += input * (_SPEED * Time.deltaTime);
                Dispatcher.Camera = _cam;
            }
        }
    }
}