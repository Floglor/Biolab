﻿using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StopRunningAction")]
    public class StopRunningAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.StopRunning();
        }
    }
}