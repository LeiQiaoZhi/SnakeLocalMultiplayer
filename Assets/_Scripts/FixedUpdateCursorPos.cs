using UnityEngine;

namespace _Scripts
{
    public class FixedUpdateCursorPos : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}
