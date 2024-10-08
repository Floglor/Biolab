﻿using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ClearTargetsAction")]

    public class ClearTargetsAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.lastTargetedMate = null;
            controller.targetCreature = null;
        }
    }
}