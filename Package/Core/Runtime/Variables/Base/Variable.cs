using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace SOA.Base
{
    //TODO Move to separate file
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
            Register();
        }

        public abstract void Register();
    }

    //(TODO Move this region to a separate file)

    #region Reference Uses Data Structure

    public class ReferenceUse
    {
        private IRegisteredReferenceContainer _container;

        //TODO Implement this/add this to params
        private ContainerType _containerType;
        private HashSet<IRegisteredReference> _references = new HashSet<IRegisteredReference>();

        public ReferenceUse(IRegisteredReferenceContainer container, HashSet<IRegisteredReference> references,
            ContainerType containerType)
        {
            this._container = container;
            this._references = references;
            this._containerType = containerType;
        }

        public IRegisteredReferenceContainer Container
        {
            get => _container;
            set => _container = value;
        }

        public ContainerType ContainerType
        {
            get => _containerType;
            set => _containerType = value;
        }

        public HashSet<IRegisteredReference> References
        {
            get => _references;
            set => _references = value;
        }

        public void AddReference(IRegisteredReference reference)
        {
            if (_references.Contains(reference))
                return;
            _references.Add(reference);
        }

        public void AddReferences(IRegisteredReference[] references)
        {
            foreach (var reference in references)
                AddReference(reference);
        }

        public void RemoveReference(IRegisteredReference reference)
        {
            if (!_references.Contains(reference))
                return;
            _references.Remove(reference);
        }
    }

    public enum ContainerType
    {
        PrefabComponent,
        NonPrefabComponent,
        ScriptableObject,
        Other
    }

    public static class IRegisteredReferenceContainerExtensions
    {
        public static ContainerType GetContainerType(this IRegisteredReferenceContainer use)
        {
            ContainerType containerType;
            try
            {
                if (use is Component pc && pc.gameObject.scene.rootCount == 0)
                    containerType = ContainerType.PrefabComponent;
                else if (use is Component npc && npc.gameObject.scene.rootCount != 0)
                    containerType = ContainerType.NonPrefabComponent;
                else if (use is ScriptableObject)
                    containerType = ContainerType.ScriptableObject;
                else
                    containerType = ContainerType.Other;
            }
            catch (UnityException)
            {
                containerType = ContainerType.Other;
            }
            catch (MissingReferenceException)
            {
                containerType = ContainerType.Other;
            }

            return containerType;
        }
    }

    public class ReferenceUses
    {
        private List<ReferenceUse> _uses = new List<ReferenceUse>();

        public List<ReferenceUse> Uses => _uses;

        public void Add(IRegisteredReferenceContainer container, IRegisteredReference reference)
        {
            //If collection is empty
            var item = new ReferenceUse
            (
                container,
                new HashSet<IRegisteredReference>() {reference},
                container.GetContainerType()
            );
            if (!_uses.Any())
                //Add new use
                _uses.Add(item);
            //Else if this container is not yet in collection
            else if (_uses.All(x => x.Container as Object != container as Object))
                //Add new use
                _uses.Add(item);
            //Else if this container is in collection but none of them match type
            else if (_uses.Where(x => x.Container as Object == container as Object)
                    .All(x => x.Container.GetContainerType() != container.GetContainerType()))
                //Add new use
                _uses.Add(item);
            else
                //Increment references in collection and of the type
                _uses.FirstOrDefault(x =>
                        x.Container == container &&
                        x.Container.GetContainerType() == container.GetContainerType())?
                    .References.Add(reference);
        }

        public bool Remove(IRegisteredReferenceContainer container, IRegisteredReference reference)
        {
            if (!_uses.Any()) return false;
            //If this container is not yet in collection
            if
            (
                _uses.All(x =>
                    x.Container != container)
            )
            {
                return false;
            }
            //If this container is in collection but none of them match type
            else if
            (
                _uses.Where(x =>
                    x.Container == container).Any(x =>
                    x.Container.GetContainerType() != container.GetContainerType())
            )
            {
                return false;
            }
            else
            {
                //Decrement references in collection and of the type
                var referenceRemoved = _uses.First(x =>
                    x.Container == container
                    && x.Container.GetContainerType() == container.GetContainerType()).References.Remove(reference);
                if (referenceRemoved)
                {
                    //Remove uses without references in them
                    var usesToKeep = new List<ReferenceUse>();
                    foreach (var use in _uses)
                        if (use.References.Any())
                            usesToKeep.Add(use);

                    _uses = usesToKeep;
                }

                return referenceRemoved;
            }
        }
    }

    #endregion

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