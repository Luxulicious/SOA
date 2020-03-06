using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class Variable<T, E, EE> : ScriptableObject
        where EE : UnityEvent<T, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] protected T _defaultValue;
        [SerializeField] protected T _runtimeValue;
        [SerializeField] protected Persistence _persistence;

        [Tooltip("Invokes an event with the current value as an argument")] [SerializeField]
        protected E _onChangeEvent = new E();

        [Tooltip("Invokes an event with the previous value and the current value as arguments")] [SerializeField]
        protected EE _onChangeWithHistoryEvent = new EE();

        public virtual T DefaultValue
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

        public virtual T Value
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
                        if (_runtimeValue != null && prev != null)
                        {
                            if (_runtimeValue.Equals(prev))
                                return;
                        }
                        else if (_runtimeValue == null && prev == null)
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

        public virtual Persistence Persistence
        {
            get => _persistence;
#if UNITY_EDITOR
            set
            {
                if (!Application.isPlaying) _persistence = value;
                else Debug.LogError($"Persistence settings of {this.name} cannot be changed during playmode", this);
            }
#endif
        }

        private void OnEnable()
        {
            Revert();
        }

        public virtual void Revert()
        {
            _runtimeValue = _defaultValue;
        }

        public virtual void AddAutoListener(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
        {
            _onChangeEvent.AddListener(onChangeListener);
            _onChangeWithHistoryEvent.AddListener(onChangeWithHistoryListener);
        }

        public virtual void RemoveAutoSubscriber(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
        {
            _onChangeEvent.AddListener(onChangeListener);
            _onChangeWithHistoryEvent.AddListener(onChangeWithHistoryListener);
        }

        #region Forceful Methods For Event invocation 

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