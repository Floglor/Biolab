using System.Collections.Generic;
using Game.Core.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class WindowGraph : MonoBehaviour
{
    private RectTransform _graphContainer;
    private static string CONTAINER_NAME = "GraphContainer";
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private MultiLineRenderer2D _lineRenderer;

    private List<int> _valueList;
    private Vector2 _lastPosition;
    private Vector2 _lastSize;
    private float _yMaximum = 100f;
    private float _xSize = 8f;
    
    private const double MYSTERIOUS_ADJUSTMENT = 0.1;


    private void Awake()
    {
        _graphContainer = transform.Find(CONTAINER_NAME).GetComponent<RectTransform>();

        _valueList = new List<int>() { 5, 20, 12, 15, 61, 12, 12, 16, 65, 58, 65, 99 };

        _lastPosition = _graphContainer.anchoredPosition;
        _lastSize = _graphContainer.sizeDelta;

        ShowGraph(_valueList);
    }

    private bool GraphMovedOrResized()
    {
        // Check if position or size has changed
        if (_graphContainer.anchoredPosition != _lastPosition || _graphContainer.sizeDelta != _lastSize)
        {
            _lastPosition = _graphContainer.anchoredPosition;
            _lastSize = _graphContainer.sizeDelta;
            return true;
        }
        return false;
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(_graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(3, 3);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = _graphContainer.sizeDelta.y;
        _lineRenderer.Points.Clear();
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = _xSize + i * _xSize;
            float yPosition = (valueList[i] / _yMaximum) * graphHeight;
            Vector2 anchoredPosition = new Vector2(xPosition, yPosition);
            GameObject circleGameObject = CreateCircle(anchoredPosition);
            Vector3 adjustedPosition = circleGameObject.GetComponent<RectTransform>().position;
            adjustedPosition.y = (float) (adjustedPosition.y + MYSTERIOUS_ADJUSTMENT);
            CreateDotLineRenderer(adjustedPosition);
        }
    }

    private void CreateDotLineRenderer(Vector2 dotPosition)
    {
        _lineRenderer.Points.Add(dotPosition);
    }

    private void RedrawGraph()
    {
        ShowGraph(_valueList);
    }
}