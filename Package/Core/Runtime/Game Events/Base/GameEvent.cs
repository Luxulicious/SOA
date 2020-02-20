using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    [CreateAssetMenu(fileName = "New Event", menuName = "SOA/Primitives/Void/Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private UnityEvent _event = new UnityEvent();

        public virtual void Invoke()
        {
            _event.Invoke();
        }

        public void AddAutoListener(UnityAction listener)
        {
            _event.AddListener(listener);
        }

        public void RemoveAutoListener(UnityAction listener)
        {
            _event.RemoveListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            _event.RemoveListener(listener);
        }

        public void AddListener(UnityAction listener)
        {
            _event.AddListener(listener);
        }
    }

    public abstract class GameEvent<E, T> : ScriptableObject where E : UnityEvent<T>, new()
    {
        [SerializeField] protected E _event = new E();

        public virtual void Invoke(T t0)
        {
            _event.Invoke(t0);
        }

        public void AddAutoListener(UnityAction<T> listener)
        {
            _event.AddListener(listener);
        }

        public void RemoveAutoListener(UnityAction<T> listener)
        {
            _event.RemoveListener(listener);
        }

        public void RemoveListener(UnityAction<T> listener)
        {
            _event.RemoveListener(listener);
        }

        public void AddListener(UnityAction<T> listener)
        {
            _event.AddListener(listener);
        }
    }
}