using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    public abstract class Variable<T, E, EE> : ScriptableObject
        where EE : UnityEvent<T, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] protected T _defaultValue;
        [SerializeField] protected T _runtimeValue;
        [SerializeField] protected Persistence _persistence;

        [SerializeField] [HideInInspector] protected bool _foldOutOnChangeEvents = false;

        [SerializeField] [HideInInspector]
        protected Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference<T, E, EE>>> _registrations =
            new Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference<T, E, EE>>>();

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
                        $"{(name.EndsWith("s", StringComparison.CurrentCultureIgnoreCase) ? name + "\'" : name + "\'s")} default value cannot be set while in playmode");
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
                        var prevValue = _runtimeValue;
                        _runtimeValue = value;
                        if (_runtimeValue != null && prevValue != null)
                        {
                            if (_runtimeValue.Equals(prevValue))
                                return;
                        }
                        else if (_runtimeValue == null && prevValue == null)
                        {
                            return;
                        }

                        InvokeOnChangeEvents(prevValue, _runtimeValue);
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

        protected virtual void InvokeOnChangeEvents(T prev, T value)
        {
            _onChangeEvent.Invoke(value);
            _onChangeWithHistoryEvent.Invoke(value, prev);
        }

        public virtual Persistence Persistence
        {
            get => _persistence;

            set
            {
                if (!Application.isPlaying) _persistence = value;
                else Debug.LogError($"Persistence settings of {name} cannot be changed during playmode", this);
            }
        }

        public Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference<T, E, EE>>>
            Registrations =>
            _registrations;

        private void OnEnable()
        {
            Revert();
        }

        public virtual void Revert()
        {
            _runtimeValue = _defaultValue;
        }

        public virtual void AddRegisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener,
            IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference<T, E, EE> reference)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
            if (Registrations.ContainsKey(referenceContainer))
                Registrations[referenceContainer].Add(reference);
            else
                Registrations.Add(referenceContainer, new HashSet<IRegisteredReference<T, E, EE>>(){reference});
            Debug.Log("Registration count: " + _registrations.Count);

        }

        public virtual void AddUnregisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
        }

        private void AddListener(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
        {
            _onChangeEvent.AddListener(onChangeListener);
            _onChangeWithHistoryEvent.AddListener(onChangeWithHistoryListener);
        }

        public virtual void RemoveRegisteredAutoListener(
            UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener,
            IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference<T, E, EE> reference)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
            if (!Registrations.ContainsKey(referenceContainer)) return;
            Registrations[referenceContainer].Remove(reference);
            if (Registrations[referenceContainer].Count < 1)
                Registrations.Remove(referenceContainer);
        }

        public virtual void RemoveUnregisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener)
        {
            RemoveListener(onChangeListener, onChangeWithHistoryListener);
        }

        private void RemoveListener(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
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