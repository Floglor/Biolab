using UnityEngine;

namespace Modern_UI_Pack.Scripts.Fixes
{
    public class UIElementInFront : MonoBehaviour
    {
        void Start()
        {
            transform.SetAsLastSibling();
        }
    }
}