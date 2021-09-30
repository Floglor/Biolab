using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public abstract class Subject : MonoBehaviour
    {
        private readonly List<Observer> _observers = new List<Observer>();

        public void RegisterObserver(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Notify(object value, NotificationType notificationType)
        {
            foreach (Observer observer in _observers) observer.OnNotify(value, notificationType);
        }
    }
}