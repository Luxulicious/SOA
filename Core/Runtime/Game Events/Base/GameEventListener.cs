using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable]
    public class GameEventListener
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _reponses;

        public void OnEnable()
        {
            _gameEvent.AddListener(InvokeResponses);
        }

        public void OnDisable()
        {
            _gameEvent.RemoveListener(InvokeResponses);
        }

        private void InvokeResponses()
        {
            _reponses.Invoke();
        }
    }

    public abstract class EventListener<ESO, E, T>
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