using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
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

    public abstract class Reference<V, T, E, EE> : Reference, IRegisteredReference<V, T, E, EE>, ISerializationCallbackReceiver
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        [SerializeField] protected T _localValue;
        [SerializeField] protected V _globalValue;
        [SerializeField] protected E _onValueChangedEvent = new E();
        [SerializeField] protected EE _onValueChangedWithHistoryEvent = new EE();
        [SerializeField, HideInInspector] private bool _foldout = false;

        [SerializeField, HideInInspector] protected V _prevGlobalValue;
        [SerializeField, HideInInspector] protected bool _foldoutEvents = false;
        [SerializeField, HideInInspector] protected IRegisteredReferenceContainer registeredReferenceContainer;

        #region Constructors
        /// <summary>
        /// Creates an unregistered reference.
        /// </summary>
        public Reference()
        {
           
        }

        /// <summary>
        /// Creates an unregistered reference with a local scope.
        /// </summary>
        /// <param name="value">Local value</param>
        public Reference(T value)
        {
            _localValue = value;
            _scope = Scope.Local;
        }

        /// <summary>
        /// Creates an registered reference.
        /// </summary>
        /// <param name="value">Local value</param>
        public Reference(IRegisteredReferenceContainer registeredReferenceContainer)
        {
            this.registeredReferenceContainer = registeredReferenceContainer;
        }

        /// <summary>
        /// Creates an registered reference with a local scope.
        /// </summary>
        /// <param name="value">Local value</param>
        public Reference(IRegisteredReferenceContainer registeredReferenceContainer, T value)
        {
            this.registeredReferenceContainer = registeredReferenceContainer;
            _localValue = value;
            _scope = Scope.Local;
        }
        #endregion
        protected virtual void InvokeOnValueChanged(T currentValue)
        {
            _onValueChangedEvent?.Invoke(currentValue);
        }

        protected virtual void InvokeOnValueChangedWithHistory(T currentValue, T previousValue)
        {
            _onValueChangedWithHistoryEvent?.Invoke(currentValue, previousValue);
        }

        public virtual bool HasValue
        {
            get
            {
                if (_scope == Scope.Global)
                    return _globalValue != null;
                else if (_scope == Scope.Local)
                    return _localValue != null;
                return false;
            }
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
        
        public void AddListenerToOnValueChanged(UnityAction<T> action)
        {
            _onValueChangedEvent.AddListener(action);
        }

        public void RemoveListenerFromOnValueChanged(UnityAction<T> action)
        {
            _onValueChangedEvent.RemoveListener(action);
        }

        public void AddListenerToOnValueChangedWithHistory(UnityAction<T, T>  action)
        {
            _onValueChangedWithHistoryEvent.AddListener(action);
        }

        public void RemoveListenerFromOnValueChangedWithHistory(UnityAction<T, T> action)
        {
            _onValueChangedWithHistoryEvent.RemoveListener(action);
        }

        public virtual void OnBeforeSerialize()
        {
            //Do Nothing
        }

        public virtual void OnAfterDeserialize()
        {
            RefreshListenersToGlobalValueOnValueChangedEvents();
            RefreshRegistrationsToGlobalValue();
        }

        public virtual void RefreshRegistrationsToGlobalValue()
        {
            if (!HasRegistration())
            {
                Debug.LogWarning(
                    $"No registration found for {typeof(Reference).Name} {this}. \n" +
                    $"Please register when instancing a reference. \n" +
                    $"This can be done manually or by using {typeof(RegisteredMonoBehaviour).Name} instead of {typeof(MonoBehaviour).Name}."
                    , _globalValue);
                return;
            }
            _prevGlobalValue?.RemoveUse(registeredReferenceContainer, this); 
            _globalValue?.RemoveUse(registeredReferenceContainer, this);
            _globalValue?.AddUse(registeredReferenceContainer, this);
            _prevGlobalValue = _globalValue;
            
        }

        public virtual void RefreshListenersToGlobalValueOnValueChangedEvents()
        {
            if (_prevGlobalValue != _globalValue)
            {
                //Remove existing listeners from previous global value
                _prevGlobalValue?.RemoveListenerFromOnChange(InvokeOnValueChanged);
                _prevGlobalValue?.RemoveListenerFromOnChangeWithHistory(InvokeOnValueChangedWithHistory);
                //Remove existing listeners from current global value
                _globalValue?.RemoveListenerFromOnChange(InvokeOnValueChanged);
                _globalValue?.RemoveListenerFromOnChangeWithHistory(InvokeOnValueChangedWithHistory);
                //Add listeners to current global Value
                _globalValue?.AddListenerToOnChange(InvokeOnValueChanged);
                _globalValue?.AddListenerToOnChangeWithHistory(InvokeOnValueChangedWithHistory);
            }
        }

        public void Register(IRegisteredReferenceContainer registeredReferenceContainer)
        {
            this.registeredReferenceContainer = registeredReferenceContainer;
            RefreshRegistrationsToGlobalValue();
        }

        public bool HasRegistration()
        {
            return registeredReferenceContainer != null;
        }

        public void Ping(V variable)
        {
#if UNITY_EDITOR
            if (_globalValue != variable) return;
            switch (_scope)
            {
                case Scope.Local:
                    return;
                case Scope.Global:
                    Ping();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#endif
        }

        public void Select(V variable)
        {
#if UNITY_EDITOR
            if (_globalValue != variable) return;
            switch (_scope)
            {
                case Scope.Local:
                    return;
                case Scope.Global:
                    Select();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
#endif
        }

        public void Ping()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(registeredReferenceContainer as UnityEngine.Object);
#endif  
        }

        public void Select()
        {
#if UNITY_EDITOR
            Selection.activeObject = registeredReferenceContainer as UnityEngine.Object;
#endif
        }
    }
}