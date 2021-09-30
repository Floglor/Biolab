using UnityEngine;
using UnityEngine.UI;

namespace Ai
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        [HideInInspector] public Creature actingCreature;
        public float searchCooldown;
        public bool searchAvailable;
        public bool isAiActive;

        //For states
        public bool oneTimeActionsAreDone;

        public Creature lastTargetedMate;
        public Creature targetCreature;

        public bool isWaitingForMateAnswer;

        public State remainState;
        public State matingState;
        public State mateMeetingState;
        public State runFromDangerState;
        public State breedState;

        public TimersHandler timersHandler;

        public Image stateIconImage;

        private IIconHandler _iconHandler;
        public State initialState;


        private void Start()
        {
            _iconHandler = GetComponent<IIconHandler>();
            timersHandler = GetComponent<TimersHandler>();
            initialState = currentState;
            searchAvailable = true;
            searchCooldown = GlobalValues.Instance.searchFoodDelay;
            actingCreature = gameObject.GetComponent<Creature>();
            stateIconImage = GetComponentInChildren<Image>();

            _iconHandler.StartIconUpdate(this);
        }

        private void Update()
        {
            if (!isAiActive) return;

            currentState.UpdateState(this);

            CountSearchCooldown();
        }


        private void OnDrawGizmos()
        {
            if (actingCreature == null) return;

            Gizmos.color = currentState.gizmoColor;
            Gizmos.DrawWireSphere(actingCreature.transform.position, actingCreature.eyesight);
        }

        public void SetMatingState()
        {
            TransitionToState(matingState);
        }

        public void GoToInitialState()
        {
            currentState = initialState;
        }

        private void CountSearchCooldown()
        {
            if (searchAvailable) return;

            searchCooldown -= Time.deltaTime;
            if (searchCooldown <= 0) searchAvailable = true;
        }

        public void TransitionToState(State nextState)
        {
            if (nextState == remainState) return;
            oneTimeActionsAreDone = false;
            currentState = nextState;
        }

        public void CancelAllActions()
        {
            actingCreature.EatingBehaviour.CancelDrinking(actingCreature);
            actingCreature.EatingBehaviour.CancelEating(actingCreature);
        }

        public void GetAlert()
        {
            if (actingCreature.isAlert) return;
            actingCreature.isAlert = true;
            TransitionToState(runFromDangerState);
        }

        public void CalmDown()
        {
            if (!actingCreature.isAlert) return;
            actingCreature.isAlert = false;
            GoToInitialState();
        }
    }
}