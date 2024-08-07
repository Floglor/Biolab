﻿using Ai.Infrastructure;
using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Drink")]
    public class DrinkAction : Action
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.EatingBehaviour.StartDrinking(controller.actingCreature);
        }
    }
}