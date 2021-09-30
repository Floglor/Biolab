using Ai;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GoForFoodAction")]
public class GoForFoodAction : OneTimeAction
{
    public override void Act(StateController controller)
    {
        controller.actingCreature.MovingBehaviour.MoveToDestination(controller.actingCreature,
            controller.actingCreature.lastFoodPosition);
        //Debug.Log($"last food position: {controller.actingCreature.lastFoodPosition}");
    }
}