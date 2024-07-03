using UnityEngine;

namespace UI
{
    public class Scroll2D : MonoBehaviour
    {
        private Camera _camera;
        private Vector2 _mouseScroll;
        private float _maxCameraSize;
        
        private void Start()
        {
            _camera = Camera.main;
            if (_camera != null) _maxCameraSize = _camera.orthographicSize;
        }

        void Update()
        {
            _mouseScroll = Input.mouseScrollDelta;
            ScrollUpdate();
        }

        private void ScrollUpdate()
        {
            if (_mouseScroll.y == 0) return;

           // if (_mouseScroll.y - _camera.orthographicSize <= _maxCameraSize && _mouseScroll.y - _camera.orthographicSize > 0)
           if (_camera.orthographicSize + -_mouseScroll.y <= _maxCameraSize && _camera.orthographicSize + -_mouseScroll.y >= 0)
                _camera.orthographicSize -= _mouseScroll.y;
        
        }

    }
}
