using Ai;
using UnityEngine;

namespace General
{
    public class Timer
    {
        private readonly float _duration;
        public readonly TimerName TimerName;
        private float _timeRemaining;
        public bool TimerIsRunning;
        public bool TimerReset = true;
        
        
        public Timer(TimerName name, float duration)
        {
            _duration = duration;
            TimerName = name;
        }


        private void EndTimer()
        {
            TimerIsRunning = false;
        }

        public void StartTimer()
        {
            _timeRemaining = _duration;
            TimerIsRunning = true;
        }

        public void Update()
        {
            if (!TimerIsRunning) return;

            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = 0;
                EndTimer();
            }
        }
    }
}