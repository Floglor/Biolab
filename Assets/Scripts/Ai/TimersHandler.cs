using System.Collections.Generic;
using System.Linq;
using General;
using UnityEngine;

namespace Ai
{
    public enum TimerName
    {
        Mating,
        Hunt,
        Awareness,
        CalmDownTimer,
        RunAway,
        PartnerInterested
    }

    public class TimersHandler : MonoBehaviour
    {
        private HashSet<Timer> timerSet;

        private void Start()
        {
            timerSet = new HashSet<Timer>();
        }


        private void Update()
        {
            foreach (Timer timer in timerSet) timer.Update();
        }

        public bool ProceedTimer(float duration, TimerName timerName)
        {
            bool timerIsPresent = false;

            foreach (Timer timer in timerSet.Where(timer => timer.TimerName == timerName))
            {
                timer.Update();
                if (!timer.TimerIsRunning)
                {
                    timerSet.Remove(timer);
                    return true;
                }

                timerIsPresent = true;
            }

            if (!timerIsPresent) CreateTimer(timerName, duration);

            return false;
        }

        public Timer GetOrCreateTimer(TimerName timerName, float duration)
        {
            foreach (Timer timer in timerSet.Where(timer => timer.TimerName == timerName)) return timer;

            return CreateTimer(timerName, duration);
        }

        public void ResetTimer(TimerName timerName)
        {
            foreach (Timer timer in timerSet.Where(timer => timer.TimerName == timerName)) timer.StartTimer();
        }

        private Timer CreateTimer(TimerName name, float duration)
        {
            Timer timer = new Timer(name, duration);
            timerSet.Add(timer);
            //timer.StartTimer();
            return timer;
        }
    }
}