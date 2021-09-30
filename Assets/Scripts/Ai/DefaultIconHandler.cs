using System.Collections;
using UnityEngine;

namespace Ai
{
    public class DefaultIconHandler : MonoBehaviour, IIconHandler
    {
        private Coroutine iconUpdate;

        public void StartIconUpdate(StateController stateController)
        {
            iconUpdate = StartCoroutine(IconUpdateCoroutine(stateController));
        }

        public void StopIconUpdate(StateController stateController)
        {
            if (iconUpdate != null)
                StopCoroutine(iconUpdate);
        }

        public IEnumerator IconUpdateCoroutine(StateController stateController)
        {
            while (true)
            {
                stateController.stateIconImage.sprite = stateController.currentState.stateIcon;
                yield return new WaitForSeconds(GlobalValues.Instance.iconResetDelay);
            }
        }
    }
}