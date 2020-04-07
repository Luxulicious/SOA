using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    //TODO Move this to a separate file
    /// <summary>
    ///Add this class to where ever you use a reference
    ///and call SetContextObject() in OnAfterDeserialize().
    ///OnBeforeSerialize can be ignored.
    /// </summary>
    public interface IRegisteredReferenceContainer : ISerializationCallbackReceiver
    {
        void Register();
        string Name { get; }
        int GetInstanceId();
    }

    //TODO Move this to a separate file
    /// <summary>
    /// A monobehaviour alternative that allows for registering any reference.
    /// </summary>
    public abstract class RegisteredMonoBehaviour : MonoBehaviour, IRegisteredReferenceContainer
    {
        public virtual void OnBeforeSerialize()
        {
            //Do Nothing
        }

        public virtual void OnAfterDeserialize()
        {
            Register();
        }

        /// <summary>
        /// Set the registration of any reference you want registered here.
        /// </summary>
        public abstract void Register();

        string IRegisteredReferenceContainer.Name => this.name;

        public int GetInstanceId()
        {
            return this.GetInstanceID();
        }
    }

    //TODO Move these to a separate file
    public interface IRegisteredReference<T, E, EE> where E : UnityEvent<T>, new() where EE : UnityEvent<T, T>, new()
    {
        void Register(IRegisteredReferenceContainer context);
        bool HasRegistration();
        void Ping();
        void Select();
    }

    public interface IRegisteredReference<V, T, E, EE> : IRegisteredReference<T, E, EE> where V : Variable<T, E, EE> where E : UnityEvent<T>, new() where EE : UnityEvent<T, T>, new()
    {
        void Ping(V variable);
        void Select(V variable);
    }

    //TODO Move these non-"Reference" classes to a seperate file and namespace/class
    public enum Persistence
    {
        Variable,
        Constant
    }

    //TODO Move this to a separate file
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
    
    //TODO Move this to a separate file
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
        [SerializeField, HideInInspector] protected IRegisteredReferenceContainer _registration;

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
            _registration = registeredReferenceContainer;
        }

        /// <summary>
        /// Creates an registered reference with a local scope.
        /// </summary>
        /// <param name="value">Local value</param>
        public Reference(IRegisteredReferenceContainer registeredReferenceContainer, T value)
        {
            _registration = registeredReferenceContainer;
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

        public virtual void OnBeforeSerialize()
        {
            //Do Nothing
        }

        public virtual void OnAfterDeserialize()
        {
            if (!HasRegistration())
            {
                Debug.LogWarning(
                    $"No registration found for {typeof(Reference).Name} {this}. \n" +
                    $"Please register when instancing a reference. \n" +
                    $"This can be done manually or by using {typeof(RegisteredMonoBehaviour).Name} instead of {typeof(MonoBehaviour).Name}."
                    , _globalValue);
                _prevGlobalValue?.RemoveUnregisteredAutoListener(InvokeOnChangeResponses,
                    InvokeOnValueChangeWithHistoryResponses);
                _globalValue?.RemoveUnregisteredAutoListener(InvokeOnChangeResponses,
                    InvokeOnValueChangeWithHistoryResponses);
                _globalValue?.AddUnregisteredAutoListener(InvokeOnChangeResponses,
                    InvokeOnValueChangeWithHistoryResponses);
            }
            else
            {
                _prevGlobalValue?.RemoveRegisteredAutoListener
                (
                    InvokeOnChangeResponses, 
                    InvokeOnValueChangeWithHistoryResponses,
                    _registration,
                    this
                );
                _globalValue?.RemoveRegisteredAutoListener
                (
                    InvokeOnChangeResponses,
                    InvokeOnValueChangeWithHistoryResponses,
                    _registration,
                    this
                );
                _globalValue?.AddRegisteredAutoListener
                (
                    InvokeOnChangeResponses,
                    InvokeOnValueChangeWithHistoryResponses,
                    _registration,
                    this
                );
            }
            _prevGlobalValue = _globalValue;
        }

        public void Register(IRegisteredReferenceContainer registeredReferenceContainer)
        {
            _registration = registeredReferenceContainer;
        }

        public bool HasRegistration()
        {
            return _registration != null;
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
            EditorGUIUtility.PingObject(_registration as UnityEngine.Object);
#endif  
        }

        public void Select()
        {
#if UNITY_EDITOR
            Selection.activeObject = _registration as UnityEngine.Object;
#endif
        }
    }
}