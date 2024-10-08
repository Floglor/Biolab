﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modern_UI_Pack.Scripts.Button
{
    public class ButtonManagerWithIcon : MonoBehaviour, IPointerEnterHandler
    {
        // Content
        public Sprite buttonIcon;
        public string buttonText = "Button";
        public UnityEvent buttonEvent;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        UnityEngine.UI.Button buttonVar;

        // Resources
        public Image normalIcon;
        public Image highlightedIcon;
        public TextMeshProUGUI normalText;
        public TextMeshProUGUI highlightedText;
        public AudioSource soundSource;

        // Settings
        public bool useCustomContent = false;
        public bool enableButtonSounds = false;
        public bool useHoverSound = true;
        public bool useClickSound = true;

        void Start()
        {
            if (useCustomContent == false)
            {
                normalIcon.sprite = buttonIcon;
                highlightedIcon.sprite = buttonIcon;
                normalText.text = buttonText;
                highlightedText.text = buttonText;
            }

            if (buttonVar == null)
                buttonVar = gameObject.GetComponent<UnityEngine.UI.Button>();

            buttonVar.onClick.AddListener(delegate
            {
                buttonEvent.Invoke();
            });

            if (enableButtonSounds == true && useClickSound == true)
            {
                buttonVar.onClick.AddListener(delegate
                {
                    soundSource.PlayOneShot(clickSound);
                });
            }
        }

        public void UpdateUI()
        {
            normalIcon.sprite = buttonIcon;
            highlightedIcon.sprite = buttonIcon;
            normalText.text = buttonText;
            highlightedText.text = buttonText;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableButtonSounds == true && useHoverSound == true && buttonVar.interactable == true)
                soundSource.PlayOneShot(hoverSound);
        }
    }
}