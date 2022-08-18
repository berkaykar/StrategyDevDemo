using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _camOffset, _camSpeed;
    [SerializeField] private SpriteRenderer mapRenderer;

    [SerializeField] private Camera _cam;

    [SerializeField] private float _zoomStep, _minCamSize, _maxCamSize;

    private float mapMinX, mapMinY, mapMaxX, mapMaxY;

    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float _scroll = Input.GetAxis("Mouse ScrollWheel");

        PanCamera();

        if (_scroll < 0)
        {
            ZoomIn();
        }
        else if(_scroll > 0)
        {
            ZoomOut();
        }
    }

    void PanCamera()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - _camOffset)
        {
            pos.y += _camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - _camOffset)
        {
            pos.x += _camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= _camOffset)
        {
            pos.y -= _camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= _camOffset)
        {
            pos.x -= _camSpeed * Time.deltaTime;
        }

        transform.position = ClampCamera(pos);

    }

    void ZoomIn()
    {
        float newSize = _cam.orthographicSize + _zoomStep;
        _cam.orthographicSize = Mathf.Clamp(newSize, _minCamSize, _maxCamSize);

        _cam.transform.position = ClampCamera(_cam.transform.position);
    }

    void ZoomOut()
    {
        float newSize = _cam.orthographicSize - _zoomStep;
        _cam.orthographicSize = Mathf.Clamp(newSize, _minCamSize, _maxCamSize);

        _cam.transform.position = ClampCamera(_cam.transform.position);
    }

    Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = _cam.orthographicSize;
        float camWidth = _cam.orthographicSize * _cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;

        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
