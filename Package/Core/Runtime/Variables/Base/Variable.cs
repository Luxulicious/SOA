using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    /// <summary>
    /// A ScriptableObject alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredScriptableObject : ScriptableObject, IRegisteredReferenceContainer
    {
        public virtual void OnAfterDeserialize()
        {
            Register();
        }

        public virtual void OnBeforeSerialize()
        {
            //Do nothing
        }

        public abstract void Register();
    }

    public abstract class Variable<T, E, EE> : ScriptableObject, ISerializationCallbackReceiver
        where EE : UnityEvent<T, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] protected T _defaultValue;
        [SerializeField] protected T _runtimeValue;
        [SerializeField] protected Persistence _persistence;

        [SerializeField] [HideInInspector] protected bool _foldOutOnChangeEvents = false;
        [SerializeField] [HideInInspector] protected bool _foldOutUses = false;

        [SerializeField] [HideInInspector]
        protected Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference>> _uses =
            new Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference>>();

        [Tooltip("Invokes an event with the current value as an argument")] [SerializeField]
        protected E _onValueChangedEvent = new E();

        [Tooltip("Invokes an event with the previous value and the current value as arguments")] [SerializeField]
        protected EE _onValueChangedWithHistoryEvent = new EE();

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
            _onValueChangedEvent.Invoke(value);
            _onValueChangedWithHistoryEvent.Invoke(value, prev);
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

        public Dictionary<IRegisteredReferenceContainer, HashSet<IRegisteredReference>>
            Uses =>
            _uses;

        private void OnEnable()
        {
            Revert();
        }

        public virtual void Revert()
        {
            _runtimeValue = _defaultValue;
        }

        #region Listeners

        public virtual void AddListenerToOnChange(UnityAction<T> action)
        {
            _onValueChangedEvent.AddListener(action);
        }

        public virtual void RemoveListenerFromOnChange(UnityAction<T> action)
        {
            _onValueChangedEvent.RemoveListener(action);
        }

        public virtual void AddListenerToOnChangeWithHistory(UnityAction<T, T> action)
        {
            _onValueChangedWithHistoryEvent.AddListener(action);
        }

        public virtual void RemoveListenerFromOnChangeWithHistory(UnityAction<T, T> action)
        {
            _onValueChangedWithHistoryEvent.AddListener(action);
        }

        #endregion

        #region Registration

        public virtual void AddUse(IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference reference)
        {
            if (Uses.ContainsKey(referenceContainer))
                Uses[referenceContainer].Add(reference);
            else
                Uses.Add(referenceContainer, new HashSet<IRegisteredReference>() {reference});

            CleanupUses();
        }

        public virtual void RemoveUse(
            IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference reference)
        {
            if (!Uses.ContainsKey(referenceContainer)) return;
            Uses[referenceContainer].Remove(reference);
            if (Uses[referenceContainer].Count < 1)
                Uses.Remove(referenceContainer);

            CleanupUses();
        }

        private void CleanupUses()
        {
            //Remove registrations that don't have any values
            var registrationsWithoutReferences = new List<IRegisteredReferenceContainer>();
            foreach (var registration in _uses)
            {
                if (registration.Value.Count <= 0)
                {
                    registrationsWithoutReferences.Add(registration.Key);
                    continue;
                }
            }

            registrationsWithoutReferences.ForEach(x => _uses.Remove(x));
            //Remove null references from registrations
            foreach (var registration in _uses)
            {
                registration.Value.Remove(null);
            }
        }

        public virtual void OnBeforeSerialize()
        {
            //Do nothing
        }

        public virtual void OnAfterDeserialize()
        {
            CleanupUses();
        }

        #endregion

        #region Methods for forcing event invocation

        public void ForceInvokeOnChangeEvent()
        {
            _onValueChangedEvent.Invoke(_runtimeValue);
        }

        public void ForceInvokeOnChangeWithHistoryEvent()
        {
            _onValueChangedWithHistoryEvent.Invoke(_runtimeValue, _runtimeValue);
        }

        #endregion

        #region Obsolete

        [Obsolete]
        public virtual void AddRegisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener,
            IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference reference)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
            if (Uses.ContainsKey(referenceContainer))
                Uses[referenceContainer].Add(reference);
            else
                Uses.Add(referenceContainer, new HashSet<IRegisteredReference>() {reference});
        }

        [Obsolete]
        public virtual void AddUnregisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
        }

        [Obsolete]
        private void AddListener(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
        {
            _onValueChangedEvent.AddListener(onChangeListener);
            _onValueChangedWithHistoryEvent.AddListener(onChangeWithHistoryListener);
        }

        [Obsolete]
        public virtual void RemoveRegisteredAutoListener(
            UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener,
            IRegisteredReferenceContainer referenceContainer,
            IRegisteredReference reference)
        {
            AddListener(onChangeListener, onChangeWithHistoryListener);
            if (!Uses.ContainsKey(referenceContainer)) return;
            Uses[referenceContainer].Remove(reference);
            if (Uses[referenceContainer].Count < 1)
                Uses.Remove(referenceContainer);
        }

        [Obsolete]
        public virtual void RemoveUnregisteredAutoListener(UnityAction<T> onChangeListener,
            UnityAction<T, T> onChangeWithHistoryListener)
        {
            RemoveListener(onChangeListener, onChangeWithHistoryListener);
        }

        [Obsolete]
        private void RemoveListener(UnityAction<T> onChangeListener, UnityAction<T, T> onChangeWithHistoryListener)
        {
            _onValueChangedEvent.AddListener(onChangeListener);
            _onValueChangedWithHistoryEvent.AddListener(onChangeWithHistoryListener);
        }

        #endregion
    }
}