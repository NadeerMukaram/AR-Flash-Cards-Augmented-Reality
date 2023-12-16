using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;

    public const float RESOLUTION = 0.1f;

    private Line _currentLine;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (_linePrefab != null)
            {
                _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Line prefab got deleted.");
            }
        }

        if (Input.GetMouseButton(0) && _currentLine != null)
        {
            _currentLine.SetPosition(mousePos);
        }
    }
}
