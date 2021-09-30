using System.Collections.Generic;
using UnityEngine;

namespace Ai
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        public Color gizmoColor = Color.gray;
        public List<OneTimeAction> oneTimeActions;
        public List<Action> actions;
        public List<Transition> transitions;
        public Sprite stateIcon;


        private void DoOneTimeActionsIfNotDone(StateController controller)
        {
            if (controller.oneTimeActionsAreDone) return;
            foreach (OneTimeAction oneTimeAction in oneTimeActions) oneTimeAction.Act(controller);

            controller.oneTimeActionsAreDone = true;
        }

        public void UpdateState(StateController controller)
        {
            DoOneTimeActionsIfNotDone(controller);
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller)
        {
            foreach (Action action in actions) action.Act(controller);
        }


        private void CheckTransitions(StateController controller)
        {
            foreach (Transition transition in transitions)
            {
                bool decisionSucceeded = transition.decision.Decide(controller);

                controller.TransitionToState(decisionSucceeded ? transition.trueState : transition.falseState);
            }
        }
    }
}