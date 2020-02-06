﻿using System;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    [CreateAssetMenu(fileName = "New Event", menuName = "SOA/Primitives/Void/Event", order = 1)]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private UnityEvent _event = new UnityEvent();

        public void Invoke()
        {
            _event.Invoke();
        }

        public void AddAutoSubscriber(GameEventListener _autoSubscriber)
        {
            _event.AddListener(_autoSubscriber.InvokeResponses);
        }

        public void RemoveAutoSubscriber(GameEventListener _autoSubscriber)
        {
            _event.RemoveListener(_autoSubscriber.InvokeResponses);
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

    public abstract class GameEvent<E, T> : ScriptableObject where E : UnityEvent<T>
    {
        [SerializeField] protected E _event;
        [SerializeField] protected List<UnityAction<T>> _nonPersistentListeners = new List<UnityAction<T>>();

        public void Invoke(T t0)
        {
            _event.Invoke(t0);
        }

        public void RemoveListener(UnityAction<T> listener)
        {
            _event.RemoveListener(listener);
            _nonPersistentListeners.Remove(listener);
        }

        public void AddListener(UnityAction<T> listener)
        {
            _event.AddListener(listener);
            _nonPersistentListeners.Add(listener);
        }

        #region Non-persistent

        private List<UnityAction<T>> GetNonPersistentListeners()
        {
            return _nonPersistentListeners;
        }

        public int GetNonPersistentEventCount()
        {
            return _nonPersistentListeners.Count;
        }

        public string GetNonPersistentEventMethodName(int i)
        {
            return _nonPersistentListeners[i].Method.Name;
        }

        public Object GetNonPersistentTarget(int i)
        {
            return (Object) _nonPersistentListeners[i].Target;
        }

        #endregion

        #region Persistent

        public string GetPersistentEventMethodName(int i)
        {
            return _event.GetPersistentMethodName(i);
        }

        public Object GetPersistentTarget(int i)
        {
            return _event.GetPersistentTarget(i);
        }

        public int GetPersistentEventCount()
        {
            return _event.GetPersistentEventCount();
        }

        #endregion
    }
}