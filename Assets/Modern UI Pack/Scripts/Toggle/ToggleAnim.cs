using UnityEngine;

namespace Modern_UI_Pack.Scripts.Toggle
{
    public class ToggleAnim : MonoBehaviour
    {
        UnityEngine.UI.Toggle toggleObject;
        Animator toggleAnimator;

        void Start()
        {
            toggleObject = gameObject.GetComponent<UnityEngine.UI.Toggle>();
            toggleAnimator = gameObject.GetComponent<Animator>();
            toggleObject.onValueChanged.AddListener(TaskOnClick);

            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");

            else
                toggleAnimator.Play("Toggle Off");
        }

        void TaskOnClick(bool value)
        {
            if (toggleObject.isOn)
                toggleAnimator.Play("Toggle On");

            else
                toggleAnimator.Play("Toggle Off");
        }
    }
}