using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modern_UI_Pack.Scripts.Button
{
    public class ButtonManagerIcon : MonoBehaviour, IPointerEnterHandler
    {
        // Content
        public Sprite buttonIcon;
        public UnityEvent buttonEvent;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        UnityEngine.UI.Button buttonVar;

        // Resources
        public Image normalIcon;
        public Image highlightedIcon;
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
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableButtonSounds == true && useHoverSound == true && buttonVar.interactable == true)
                soundSource.PlayOneShot(hoverSound);
        }
    }
}