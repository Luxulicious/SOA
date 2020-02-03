using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable]
    public class GameEventListener
    {
        [SerializeField] protected GameEvent _gameEvent;
        [SerializeField] protected UnityEvent _responses;
        protected GameEvent _prevGameEvent = null;

        public void InvokeResponses()
        {
            _responses.Invoke();
        }

        public GameEvent PrevGameEvent
        {
            get => _prevGameEvent;
            set => _prevGameEvent = value;
        }
    }

    public abstract class GameEventListener<ESO, E, T>
        where ESO : GameEvent<E, T> where E : UnityEvent<T>
    {
        [SerializeField] protected ESO _unityEventSO;
        [SerializeField] protected E _responses;

        public void OnEnable()
        {
            if (_unityEventSO == null) return;
            _unityEventSO.AddListener(InvokeResponses);
        }

        public void OnDisable()
        {
            if (_unityEventSO == null) return;
            _unityEventSO.RemoveListener(InvokeResponses);
        }

        private void InvokeResponses(T t0)
        {
            _responses.Invoke(t0);
        }
    }
}