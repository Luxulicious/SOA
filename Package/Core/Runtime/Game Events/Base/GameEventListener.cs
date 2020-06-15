using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    [Serializable,
     Obsolete(
         "Game event listeners may be removed in the future, due to not being compatible with unity's new prefab workflow")]
    public class GameEventListener : ISerializationCallbackReceiver
    {
        [SerializeField] protected GameEvent _gameEvent;
        [SerializeField] protected UnityEvent _responses = new UnityEvent();
        protected GameEvent _prevGameEvent;

        public void InvokeResponses()
        {
            _responses.Invoke();
        }

        public GameEvent GameEvent
        {
            get { return _gameEvent;}
            set
            {
                if (_gameEvent != value)
                {
                    _prevGameEvent = _gameEvent;
                    _gameEvent = value;
                    RefreshListeners();
                }
            }
        }
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            
        }

        private void RefreshListeners()
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

        public GE GameEvent
        {
            get { return _gameEvent; }
            set
            {
                if (_gameEvent != value)
                {
                    _prevGameEvent = _gameEvent;
                    _gameEvent = value;
                    RefreshListeners();
                }
            }
        }

        protected virtual void InvokeResponses(T value)
        {
            _responses.Invoke(value);
        }

        public void AddListener(UnityAction<T> action)
        {
            _responses.AddListener(action);
        }

        public void RemoveListener(UnityAction<T> action)
        {
            _responses.RemoveListener(action);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            RefreshListeners();
        }

        private void RefreshListeners()
        {
            _prevGameEvent?.RemoveAutoListener(InvokeResponses);
            _gameEvent?.RemoveAutoListener(InvokeResponses);
            _gameEvent?.AddAutoListener(InvokeResponses);
            _prevGameEvent = _gameEvent;
        }
    }
}