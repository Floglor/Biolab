using System.Collections.Generic;
using System.Linq;
using CreatureSystems;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreatureList : MonoBehaviour
{
    public static CreatureList Instance;
    [ShowInInspector] [ReadOnly] public List<Creature> allCreatures;

    private void Awake()
    {
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