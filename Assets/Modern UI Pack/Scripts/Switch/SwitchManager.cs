﻿using UnityEngine;
using UnityEngine.Events;

namespace Modern_UI_Pack.Scripts.Switch
{
    public class SwitchManager : MonoBehaviour
    {
        // Events
        public UnityEvent OnEvents;
        public UnityEvent OffEvents;

        // Saving
        public bool saveValue = true;
        public string switchTag = "Switch";

        // Settings
        public bool isOn = true;
        public bool invokeAtStart = true;

        Animator switchAnimator;
        UnityEngine.UI.Button switchButton;

        void Start()
        {
            try
            {
                switchAnimator = gameObject.GetComponent<Animator>();
                switchButton = gameObject.GetComponent<UnityEngine.UI.Button>();
                switchButton.onClick.AddListener(AnimateSwitch);
            }

            catch
            {
                Debug.LogError("Switch - Cannot initalize the switch due to missing variables.", this);
            }

            if (saveValue == true)
            {
                if (PlayerPrefs.GetString(switchTag + "Switch") == "")
                {
                    if (isOn == true)
                    {
                        switchAnimator.Play("Switch On");
                        isOn = true;
                        PlayerPrefs.SetString(switchTag + "Switch", "true");
                    }

                    else
                    {
                        switchAnimator.Play("Switch Off");
                        isOn = false;
                        PlayerPrefs.SetString(switchTag + "Switch", "false");
                    }
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "true")
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "false")
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            else
            {
                if (isOn == true)
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            if (invokeAtStart == true && isOn == true)
                OnEvents.Invoke();
            if (invokeAtStart == true && isOn == false)
                OffEvents.Invoke();
        }

        void OnEnable()
        {
            if (switchAnimator == null)
                switchAnimator = gameObject.GetComponent<Animator>();

            if (saveValue == true)
            {
                if (PlayerPrefs.GetString(switchTag + "Switch") == "")
                {
                    if (isOn == true)
                    {
                        switchAnimator.Play("Switch On");
                        isOn = true;
                        PlayerPrefs.SetString(switchTag + "Switch", "true");
                    }

                    else
                    {
                        switchAnimator.Play("Switch Off");
                        isOn = false;
                        PlayerPrefs.SetString(switchTag + "Switch", "false");
                    }
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "true")
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else if (PlayerPrefs.GetString(switchTag + "Switch") == "false")
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }

            else
            {
                if (isOn == true)
                {
                    switchAnimator.Play("Switch On");
                    isOn = true;
                }

                else
                {
                    switchAnimator.Play("Switch Off");
                    isOn = false;
                }
            }
        }

        public void AnimateSwitch()
        {
            if (isOn == true)
            {
                switchAnimator.Play("Switch Off");
                isOn = false;
                OffEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "Switch", "false");
            }

            else
            {
                switchAnimator.Play("Switch On");
                isOn = true;
                OnEvents.Invoke();

                if (saveValue == true)
                    PlayerPrefs.SetString(switchTag + "Switch", "true");
            }
        }
    }
}