using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        public static DebugUI Instance;
        [SerializeField] private TextMeshProUGUI _debugText;

        private void Start()
        {
            Instance = this;
        }

        public void SetDebugText(string text)
        {
            _debugText.SetText(text);
        }

        public void ClearDebugText()
        {
            _debugText.SetText("");
        }
    }
}