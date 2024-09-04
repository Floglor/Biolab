using System;
using System.Collections.Generic;
using System.Linq;
using CreatureSystems;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreatureList : MonoBehaviour
{
    public static CreatureList Instance;
    [ShowInInspector] [ReadOnly] private List<Creature> allCreatures;
    
    public event Action<int, int> OnCreatureCountChanged; //int herbivores, int carnivores

    private bool _gameStarted;
    


    public void ReturnCreatures(out int carnivores, out int herbivores)
    {
        herbivores = 0;
        carnivores = 0;
        
        
        foreach (Creature allCreature in allCreatures)
        {
            if (allCreature.isHerbivore) herbivores++;
            else carnivores++;
        }
    }
    public void RegisterCreature(Creature creature)
    {
        allCreatures.Add(creature);
        
        if (!_gameStarted) return;
        
        int carnivores = 0;
        int herbivores = 0;
        
        ReturnCreatures(out carnivores, out herbivores);
        
        OnCreatureCountChanged.Invoke(herbivores, carnivores);
    }

    private void Start()
    {
        _gameStarted = true;
    }

    public void RemoveCreature(Creature creature)
    {
        allCreatures.Remove(creature);

        int carnivores = 0;
        int herbivores = 0;
        
        ReturnCreatures(out carnivores, out herbivores);
        
        OnCreatureCountChanged.Invoke(herbivores, carnivores);
    }
    private void Awake()
    {
        allCreatures = new List<Creature>();
        Instance = this;
    }

    public List<Creature> SortByDistance(Vector3 targetPosition)
    {
        if (allCreatures == null || allCreatures.Count == 0) return null;
        List<Creature> newCreatureList =
            allCreatures.OrderBy(p => Vector3.Distance(p.transform.position, targetPosition)).ToList();

        return newCreatureList;
    }
}