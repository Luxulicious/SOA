using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
   
    

    public abstract class Variable<T, E, EE> : ScriptableObject, ISerializationCallbackReceiver
        where EE : UnityEvent<T, T>, new() where E : UnityEvent<T>, new()
    {
        [SerializeField] protected T _defaultValue;
        [SerializeField] protected T _runtimeValue;
        [SerializeField] protected Persistence _persistence;

        [SerializeField] [HideInInspector] protected bool _foldOutOnChangeEvents = false;
        [SerializeField] [HideInInspector] protected bool _foldOutUses = false;

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

                        InvokeOnValueChangedEvents(_runtimeValue, prevValue);
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

        protected virtual void InvokeOnValueChangedEvents(T value, T prev)
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

        private void OnEnable()
        {
            Revert();
        }

        public virtual void Revert()
        {
            _runtimeValue = _defaultValue;
        }

        //TODO Maybe make a partial class out of this region

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

        //TODO Maybe make a partial class out of this region

        #region Registration 

        [SerializeField] [HideInInspector] protected ReferenceUses _uses =
            new ReferenceUses();

        public List<ReferenceUse> Uses => _uses.Uses;

        public virtual void AddUse(IRegisteredReferenceContainer container,
            IRegisteredReference reference)
        {
            _uses.Add(container, reference);
        }

        public virtual bool RemoveUse(
            IRegisteredReferenceContainer container,
            IRegisteredReference reference)
        {
            var removedUse = _uses.Remove(container, reference);

            return removedUse;
        }

        #endregion

        //TODO Maybe make a partial class out of this region

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

        public virtual void OnBeforeSerialize()
        {
        }

        public virtual void OnAfterDeserialize()
        {
        }
    }
}