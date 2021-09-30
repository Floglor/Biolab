using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TopDown2DCameraController : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private Camera _camera;
    private Vector2 _mouseScroll;
    private float _maxCameraSize;
    private bool _blockYMovement;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _maxCameraSize = _camera.orthographicSize;
    }

    void Update()
    {
        DragUpdate();

        _mouseScroll = Input.mouseScrollDelta;
        
        ScrollUpdate();
    }

    private void ScrollUpdate()
    {
        if (_mouseScroll.y == 0) return;

            if (_mouseScroll.y + _camera.orthographicSize <= _maxCameraSize && _mouseScroll.y + _camera.orthographicSize > 0)
            _camera.orthographicSize += _mouseScroll.y;
        
    }

    private void DragUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        
        
        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = _camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        
        if (CheckForBorders(move)) return;

        transform.Translate(move, Space.World);
    }

    private bool CheckForBorders(Vector3 move)
    {
        if (_camera.transform.position.y + move.y > 25) return true;
        if (_camera.transform.position.y + move.y < -25) return true;
        if (_camera.transform.position.x + move.y > 25) return true;
        if (_camera.transform.position.x + move.y < -25) return true;
        return false;
    }
}
