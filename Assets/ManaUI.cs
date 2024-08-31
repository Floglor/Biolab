using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Skills;
using TMPro;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    private SkillHolder _skillHolder;
    private TextMeshProUGUI _text; 

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _skillHolder = FindObjectOfType<SkillHolder>();
        _skillHolder.OnManaChanged += OnManaChanged;
    }

    private void OnManaChanged(int mana)
    {
        _text.text = mana.ToString();
    }
}
