using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float radius;
    

    [Button]
    private void DrawCircle(int steps, float radius)
    {
        _lineRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float) currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x,y, 0);

            _lineRenderer.SetPosition(currentStep, currentPosition);
        }
    }
}
