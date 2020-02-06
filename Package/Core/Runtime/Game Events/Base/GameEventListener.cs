using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable]
    public class GameEventListener : ISerializationCallbackReceiver
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _responses;
        [SerializeField] private GameEvent _prevGameEvent;


        public void InvokeResponses()
        {
            _responses.Invoke();
        }


        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _prevGameEvent?.RemoveAutoListener(InvokeResponses);
            _gameEvent?.RemoveAutoListener(InvokeResponses);
            _gameEvent?.AddAutoListener(InvokeResponses);
            _prevGameEvent = _gameEvent;
        }
    }

    public abstract class GameEventListener<GE, E, T>
        where GE : GameEvent<E, T> where E : UnityEvent<T>
    {
        [SerializeField] protected GE _gameEvent;
        [SerializeField] protected E _responses;

        public void OnEnable()
        {
            if (_gameEvent == null) return;
            _gameEvent.AddListener(InvokeResponses);
        }

        public void OnDisable()
        {
            if (_gameEvent == null) return;
            _gameEvent.RemoveListener(InvokeResponses);
        }

        private void InvokeResponses(T t0)
        {
            _responses.Invoke(t0);
        }
    }
}