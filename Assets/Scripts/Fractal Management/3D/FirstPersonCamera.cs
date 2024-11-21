using UnityEngine;

namespace Fractals
{
    /// <summary> Controls the position and orientation of a 3D cam </summary>
    [RequireComponent(typeof(Camera))]
    public class FirstPersonCamera : MonoBehaviour
    {
        const float _SPEED = 3.75f;

        const float _ROT_SPEED = 70;

        Vector3 Forward => transform.forward.With(y: 0).normalized;

        Vector3 Right => transform.right.With(y: 0).normalized;

        void Update()
        {
            var input = Vector3.zero;

            if (Input.GetKey(KeyCode.W)) input += Forward;
            if (Input.GetKey(KeyCode.S)) input -= Forward;
            if (Input.GetKey(KeyCode.D)) input += Right;
            if (Input.GetKey(KeyCode.A)) input -= Right;
            if (Input.GetKey(KeyCode.Q)) input += Vector3.up;
            if (Input.GetKey(KeyCode.E)) input -= Vector3.up;

            var rot = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow)) rot += Vector3.left;
            if (Input.GetKey(KeyCode.DownArrow)) rot += Vector3.right;
            if (Input.GetKey(KeyCode.RightArrow)) rot += Vector3.up;
            if (Input.GetKey(KeyCode.LeftArrow)) rot += Vector3.down;

            if (input != Vector3.zero || rot != Vector3.zero)
            {
                transform.position += input * (_SPEED * Time.deltaTime);
                transform.position = transform.position.WithMagnitudeClamped(2.1f, 5.5f);
                transform.eulerAngles += rot * (_ROT_SPEED * Time.deltaTime);
            }
        }
    }
}