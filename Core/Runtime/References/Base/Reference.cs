using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    //TODO Move these non-"Reference" classes to a seperate file and namespace/class
    public enum Persistence
    {
        Variable,
        Constant
    }

    [Serializable]
    public class PersistenceException : Exception
    {
        public PersistenceException()
        {

        }

        public PersistenceException(string message)
            : base(message)
        {

        }


    }

    public enum Scope
    {
        Local,
        Global
    }

    public abstract class Reference
    {
        [SerializeField] protected Scope _scope = Scope.Global;
        [SerializeField] protected Persistence _persistence = Persistence.Variable;

        public Scope Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        public Persistence Persistence
        {
            get { return _persistence; }
            set { _persistence = value; }
        }
    }

    public abstract class Reference<V, T, E, EE> : Reference, ISerializationCallbackReceiver
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        [SerializeField] protected T _localValue;
        [SerializeField] protected V _globalValue;
        [SerializeField] protected E _onValueChangedEvent = new E();
        [SerializeField] protected EE _onValueChangedWithHistoryEvent = new EE();
        [SerializeField, HideInInspector] private bool _foldout = false;

        [SerializeField, HideInInspector] private V _prevGlobalValue;
        [SerializeField, HideInInspector] private bool _foldoutEvents = false;

        public Reference()
        {
        }

        public Reference(T value)
        {
            _localValue = value;
            _scope = Scope.Local;
        }

        protected virtual void InvokeOnChangeResponses(T currentValue)
        {
            _onValueChangedEvent?.Invoke(currentValue);
        }

        protected virtual void InvokeOnValueChangeWithHistoryResponses(T currentValue, T previousValue)
        {
            _onValueChangedWithHistoryEvent?.Invoke(currentValue, previousValue);
        }

        public V GlobalValue
        {
            get { return _globalValue; }
            set { _globalValue = value; }
        }

        public T LocalValue
        {
            get { return _localValue; }
            set { _localValue = value; }
        }

        public virtual T Value
        {
            get
            {
                if (_scope == Scope.Global)
                    return _globalValue.Value;
                else if (_scope == Scope.Local)
                    return _localValue;
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                switch (_persistence)
                {
                    case Persistence.Constant:
                        throw new PersistenceException(
                            $"Cannot set {_scope.ToString()} value, because it's persistence is set to be constant.");
                        break;
                    case Persistence.Variable:
                        switch (_scope)
                        {
                            case Scope.Local:
                                var prevValue = _localValue;
                                _localValue = value;

                                if (_localValue.Equals(prevValue))
                                    return;
                                _onValueChangedEvent.Invoke(_localValue);
                                _onValueChangedWithHistoryEvent.Invoke(_localValue, prevValue);
                                break;
                            case Scope.Global:
                                if (_globalValue != null)
                                    _globalValue.Value = value;
                                else
                                    throw new NullReferenceException(
                                        $"Value of variable cannot be assigned to because the variable is null.");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public bool EqualsValue(object obj)
        {
            return ((T) obj).Equals(Value);
        }
        
        public void AddListener(UnityAction<T> action)
        {
            _onValueChangedEvent.AddListener(action);
        }

        public void RemoveListener(UnityAction<T> action)
        {
            _onValueChangedEvent.RemoveListener(action);
        }

        public void AddListener(UnityAction<T, T>  action)
        {
            _onValueChangedWithHistoryEvent.AddListener(action);
        }

        public void RemoveListener(UnityAction<T, T> action)
        {
            _onValueChangedWithHistoryEvent.RemoveListener(action);
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _prevGlobalValue?.RemoveAutoSubscriber(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses);
            _globalValue?.RemoveAutoSubscriber(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses);
            _globalValue?.AddAutoListener(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses);
            _prevGlobalValue = _globalValue;
        }
    }
}