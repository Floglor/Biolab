using UnityEngine;

namespace Gameplay.Skills
{
    public class MouseSnapping : MonoBehaviour
    {
        private GameObject _snappedObject;

        public void StartSnapping(GameObject objectToSnap)
        {
            _snappedObject = objectToSnap;
            _snappedObject.SetActive(true);  // Ensure it's visible
        }

        public void StopSnapping()
        {
            if (_snappedObject != null)
            {
                _snappedObject.SetActive(false);  // Hide when snapping stops
                _snappedObject = null;
            }
        }

        private void Update()
        {
            if (_snappedObject != null)
            {
                Vector2 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                _snappedObject.transform.position = mousePosition;
            }
        }
    }
}