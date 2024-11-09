using UnityEngine;

namespace Fractals
{
    [RequireComponent(typeof(Camera))]
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] FractalDispatcher3D Dispatcher;

        Camera _cam;

        const float _SPEED = 5;

        const float _ROT_SPEED = 90;

        Vector3 Forward => transform.forward.With(y: 0).normalized;

        Vector3 Right => transform.right.With(y: 0).normalized;

        void Start() => _cam = GetComponent<Camera>();

        void Update()
        {
            var input = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) input += Forward;
            if (Input.GetKey(KeyCode.S)) input -= Forward;
            if (Input.GetKey(KeyCode.D)) input += Right;
            if (Input.GetKey(KeyCode.A)) input -= Right;
            if (Input.GetKey(KeyCode.Q)) input += Vector3.up;
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.LeftCommand)) input -= Vector3.up;

            var rot = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow)) rot += Vector3.left;
            if (Input.GetKey(KeyCode.DownArrow)) rot += Vector3.right;
            if (Input.GetKey(KeyCode.RightArrow)) rot += Vector3.up;
            if (Input.GetKey(KeyCode.LeftArrow)) rot += Vector3.down;

            if (input != Vector3.zero || rot != Vector3.zero)
            {
                transform.position += input * (_SPEED * Time.deltaTime);
                transform.eulerAngles += rot * (_ROT_SPEED * Time.deltaTime);

                Dispatcher.Camera = _cam;
            }
        }
    }
}