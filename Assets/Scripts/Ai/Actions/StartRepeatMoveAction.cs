﻿using UnityEngine;

namespace Ai.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/StartRepeatMoveAction")]
    public class StartRepeatMoveAction : OneTimeAction
    {
        public override void Act(StateController controller)
        {
            controller.actingCreature.RepeatMoveBehaviour.StartRepeatMove(controller.actingCreature,
                controller.lastTargetedMate.transform);
        }
    }
}