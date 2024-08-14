using Ai;
using Stats;
using UnityEngine;

public class GeneStatNeedsDecay : MonoBehaviour, INeedsDecay
{

    public void NeedsDecayTick(GOStatContainer statContainer, CreatureState creatureState)
    {
        
        float energyConsumption = statContainer.GetStat(StatName.EnergyConsumptionPerSecond);
        
        float baseEnergyExpense = energyConsumption;

        
        float activityEnergyExpense = 0f;
        
      // switch (creatureState)
      // {
      //     case CreatureState.Running:
      //         float runSpeed = statContainer.GetStat(StatName.RunSpeed);
      //         activityEnergyExpense = runSpeed * 0.2f; // Example multiplier for running
      //         break;

      //     case CreatureState.Sprinting:
      //         float sprintSpeed = statContainer.GetStat(StatName.SprintSpeed);
      //         activityEnergyExpense = sprintSpeed * 0.5f; // Example multiplier for sprinting
      //         break;

      //     case CreatureState.Idle:
      //     default:
      //         // Idle state consumes only the base energy expense
      //         activityEnergyExpense = 0f;
      //         break;
      // }
        
        
    }
}