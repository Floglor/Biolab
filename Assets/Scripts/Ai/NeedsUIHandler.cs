using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface INeedsUI
{
    void UpdateHunger(float hunger, float maxHunger);
    void UpdateThirst(float thirst, float maxThirst);
}
public class NeedsUIHandler : MonoBehaviour, INeedsUI
{
    public Image hungerImage;
    public Image thirstImage;
    
    public void UpdateHunger(float hunger, float maxHunger)
    {
        hungerImage.fillAmount = hunger / maxHunger;
    }

    public void UpdateThirst(float thirst, float maxThirst)
    {
        thirstImage.fillAmount = thirst / maxThirst;
    }
}
