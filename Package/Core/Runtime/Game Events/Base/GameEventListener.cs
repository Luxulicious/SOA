using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable]
    public class GameEventListener : ISerializationCallbackReceiver
    {
        [SerializeField] protected GameEvent _gameEvent;
        [SerializeField] protected UnityEvent _responses;
        protected GameEvent _prevGameEvent;


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

    public abstract class GameEventListener<GE, E, T> : ISerializationCallbackReceiver
        where GE : GameEvent<E, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] protected GE _gameEvent;
        [SerializeField] protected E _responses;
        protected GE _prevGameEvent;


        private void InvokeResponses(T value)
        {
            _responses.Invoke(value);
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
}