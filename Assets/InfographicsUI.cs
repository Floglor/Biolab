using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfographicsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _carnivoreNumber;
    [SerializeField] private TextMeshProUGUI _herbivoreNumber;
    [SerializeField] private TextMeshProUGUI _carcassNumber;
    [SerializeField] private TextMeshProUGUI _plantPercentage;
    [SerializeField] private int _foodUpdateFrequency;
    private float _foodUpdateTimer;

    private int _carnivoresNumber;
    private int _herbivoresNumber;

    private float _startingFood;
    private float _currentFood;

    private CreatureList _creatureList;

    private void Update()
    {
        _foodUpdateTimer -= Time.deltaTime;

        CheckTimerUpdate();
    }

    private void CheckTimerUpdate()
    {
        if (_foodUpdateTimer <= 0)
        {
            _foodUpdateTimer = _foodUpdateFrequency;
            
            UpdatePlantInfo();
        }
    }
    
    private void UpdatePlantInfo()
    {
        _currentFood = FoodController.Instance.GetTotalFood();

        _plantPercentage.text = $"{(int) ((_currentFood/_startingFood)*100)}%";
    }

    private void Start()
    {
        _foodUpdateTimer = _foodUpdateFrequency;
        _creatureList = CreatureList.Instance;

        _creatureList.OnCreatureCountChanged += UpdateCreatureCount;
        
        _creatureList.ReturnCreatures(out _carnivoresNumber, out _herbivoresNumber);

        _carnivoreNumber.text = _carnivoresNumber.ToString();
        _herbivoreNumber.text = _herbivoresNumber.ToString();

        _startingFood = FoodController.Instance.GetTotalFood();
        _currentFood = _startingFood;
    }

    public void UpdateCreatureCount(int herbivores, int carnivores)
    {
        _carnivoresNumber = carnivores;
        _herbivoresNumber = herbivores;
        
        _carnivoreNumber.text = _carnivoresNumber.ToString();
        _herbivoreNumber.text = _herbivoresNumber.ToString();
    }
}
