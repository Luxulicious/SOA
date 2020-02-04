﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class Variable<T, E, EE> : ScriptableObject
        where EE : UnityEvent<T, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] private T _defaultValue;
        [SerializeField] private T _runtimeValue;
        [SerializeField] private Persistence _persistence;

        [Tooltip("Invokes an event with the current value as an argument")] [SerializeField]
        protected E _onChangeEvent = new E();

        [Tooltip("Invokes an event with the previous value and the current value as arguments")] [SerializeField]
        protected EE _onChangeWithHistoryEvent = new EE();

        public T DefaultValue
        {
            get => _defaultValue;
#if UNITY_EDITOR
            set
            {
                if (!Application.isPlaying) _defaultValue = value;
                else
                    Debug.LogError(
                        $"{(this.name.EndsWith("s", StringComparison.CurrentCultureIgnoreCase) ? (this.name + "\'") : (this.name + "\'s"))} default value cannot be set while in playmode");
            }
#endif
        }


        public T Value
        {
            get => _runtimeValue;
            set
            {
                switch (_persistence)
                {
                    case Persistence.Variable:
                    {
                        var prev = _runtimeValue;
                        _runtimeValue = value;
                        if (_runtimeValue.Equals(prev))
                            return;
                        _onChangeEvent.Invoke(_runtimeValue);
                        _onChangeWithHistoryEvent.Invoke(_runtimeValue, prev);
                        break;
                    }

                    case Persistence.Constant:
                        Debug.LogError($"{name} is being used as a constant and cannot be edited");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Persistence Persistence
        {
            get => _persistence;
#if UNITY_EDITOR
            set
            {
                if (!Application.isPlaying) _persistence = value;
                else Debug.LogError($"Persitence settings of {this.name} cannot be changed during playmode", this);
            }
#endif
        }

        private void OnEnable()
        {
            _runtimeValue = _defaultValue;
        }

        #region Event listening

        public void AddListenerFromOnChangeEvent(UnityAction<T> listener)
        {
            if (listener == null) return;
            _onChangeEvent?.AddListener(listener);
        }

        public void AddListenerFromOnChangeWithHistoryEvent(UnityAction<T, T> listener)
        {
            if (listener == null) return;
            _onChangeWithHistoryEvent?.AddListener(listener);
        }

        public bool RemoveListenerFromOnChangeEvent(UnityAction<T> listener)
        {
            try
            {
                _onChangeEvent.RemoveListener(listener);
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return true;
        }

        public void RemoveListenerFromOnChangeWithHistoryEvent(UnityAction<T, T> listener)
        {
            _onChangeWithHistoryEvent.RemoveListener(listener);
        }

        #endregion

        #region Test Methods For Event invocation 

        public void ForceInvokeOnChangeEvent()
        {
            _onChangeEvent.Invoke(_runtimeValue);
        }

        public void ForceInvokeOnChangeWithHistoryEvent()
        {
            _onChangeWithHistoryEvent.Invoke(_runtimeValue, _runtimeValue);
        }

        #endregion
    }
}