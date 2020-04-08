using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using JetBrains.Annotations;
using SOA.Base;
using SOA.Common.Primitives;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using Scope = SOA.Base.Scope;

namespace SOA.Common.Primitives
{
    //TODO This class has become rather big and should probably be split up into partial classes. Use the given regions for this.
    [CreateAssetMenu(fileName = "New Bool Variable", menuName = "SOA/Primitives/Bool/Variable", order = 1)]
    public class BoolVariable : Variable<bool, BoolUnityEvent, BoolBoolUnityEvent>, ISerializationCallbackReceiver,
        IRegisteredReferenceContainer
    {
        [SerializeField] protected BoolUnityEvent _onValueChangedToTrueEvent = new BoolUnityEvent();
        [SerializeField] protected BoolUnityEvent _onValueChangedToFalseEvent = new BoolUnityEvent();
        [SerializeField] protected UnityEvent _onAfterDeserializedEvent = new UnityEvent();

        #region Composite fields

        [SerializeField] [Tooltip("Value will be based on a composite of one or more referenced variables")]
        protected bool _composite = false;

        [SerializeField] protected bool _compositeValue;
        [SerializeField] protected BooleanOperator _andOr;

        [SerializeField] [HideInInspector] protected List<BoolReference> _prevMemberValues = new List<BoolReference>();

        //TODO Should ideally be a hashset to avoid duplicates, but unity serialization issues...
        [SerializeField] protected List<BoolReference> _memberValues = new List<BoolReference>(1);

        #endregion

        #region Overriden variable methods

        public override bool Value
        {
            get
            {
                if (_composite) return CompositeValue;
                return base.Value;
            }
            set
            {
                if (_composite)
                {
                    UpdateCompositeValue();
                    throw new CompositeException(
                        $"Setting the value directly of type {GetType().Name} is impossible. Use member values instead.");
                }
                else
                {
                    base.Value = value;
                }
            }
        }

        public override bool DefaultValue
        {
            get
            {
                if (_composite)
                    throw new CompositeException(
                        $"The default value of {GetType().Name} is not used in a composite context.");
                else
                    return base.DefaultValue;
            }
            set
            {
                if (_composite)
                    throw new CompositeException(
                        $"Setting the default value directly of type {GetType().Name} is impossible.");
                else
                    base.DefaultValue = value;
            }
        }

        public override Persistence Persistence
        {
            get
            {
                if (_composite)
                {
                    switch (_persistence)
                    {
                        case Persistence.Constant:
                            Debug.LogWarning($"{typeof(Persistence).Name} of type {GetType().Name} is always variable",
                                this);
                            _persistence = Persistence.Variable;
                            break;
                        case Persistence.Variable:
                            break;
                        default:
                            Debug.LogWarning($"{typeof(Persistence).Name} of type {GetType().Name} is always variable",
                                this);
                            _persistence = Persistence.Variable;
                            break;
                    }

                    return _persistence;
                }
                else
                {
                    return base.Persistence;
                }
            }
            set
            {
                if (_composite)
                {
                    if (value == Persistence.Variable)
                    {
                        Debug.LogError($"{typeof(Persistence).Name} of type {GetType().Name} is always variable", this);
                        _persistence = Persistence.Variable;
                    }
                    else
                    {
                        throw new CompositeException(
                            $"{typeof(Persistence).Name} of type {GetType().Name} can only be variable");
                    }
                }
                else
                {
                    base.Persistence = value;
                }
            }
        }

        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            _onAfterDeserializedEvent.Invoke();
            if (!_composite) return;
            //Register as a container
            Register();
            //Refresh connections to member values for when to update the composite
            _prevMemberValues.ForEach(x =>
            {
                x.RemoveListenerFromOnValueChanged(UpdateCompositeValue);
                x.GlobalValue.RemoveListenerFromOnAfterDeserializedEvent(UpdateCompositeValue);
            });
            _memberValues.ForEach(x =>
            {
                x.RemoveListenerFromOnValueChanged(UpdateCompositeValue);
                x.GlobalValue.RemoveListenerFromOnAfterDeserializedEvent(UpdateCompositeValue);
            });
            _memberValues.ForEach(x =>
            {
                x.AddListenerToOnValueChanged(UpdateCompositeValue);
                x.GlobalValue.AddListenerToOnAfterDeserializedEvent(UpdateCompositeValue);
            });
            _prevMemberValues = _memberValues;
            try
            {
                UpdateCompositeValue();
            }
            catch (CompositeException e)
            {
                Debug.LogError(e.Message, this);
            }
        }

        #endregion

        #region Composite methods

        public bool CompositeValue
        {
            get
            {
                if (!_composite)
                    throw new CompositeException(
                        $"Cannot get composite value, {typeof(BoolVariable)} {name} was not set to composite.");
                UpdateCompositeValue();
                return _compositeValue;
            }
        }

        //TODO Replace this by converting the UnityAction<bool> to UnityAction somehow
        /// <summary>
        /// Updates the composite value according to member values
        /// </summary>
        /// <param name="b">This is an overload of a parameterless method of the same name, this parameter does nothing.</param>
        private void UpdateCompositeValue(bool b)
        {
            UpdateCompositeValue();
        }

        /// <summary>
        /// Updates the composite value according to member values
        /// </summary>
        public void UpdateCompositeValue()
        {
            try
            {
                if (!ValidateComposite()) return;
            }
            catch (CompositeException e)
            {
                Debug.LogError(e.Message, this);
                return;
            }

            var prev = _compositeValue;
            var result = false;
            result = CalculateCompositeValue();
            _compositeValue = result;

            try
            {
                if (Application.isPlaying)
                    if (prev != _compositeValue)
                    {
                        base.InvokeOnValueChangedEvents(_compositeValue, prev);
                        if (!_compositeValue)
                            _onValueChangedToFalseEvent.Invoke(false);
                        else _onValueChangedToTrueEvent.Invoke(true);
                    }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
                throw;
            }
        }

        /// <summary>
        /// Calculates the composite value based of member values
        /// </summary>
        /// <returns></returns>
        private bool CalculateCompositeValue()
        {
            bool result;
            if (_memberValues.Count == 1)
                result = _memberValues.First().Value;
            else if (_memberValues.Count > 1)
                switch (_andOr)
                {
                    case BooleanOperator.And:
                        result = true;
                        foreach (var x in _memberValues)
                        {
                            if (x.Value) continue;
                            result = false;
                            break;
                        }

                        break;
                    case BooleanOperator.Or:
                        result = false;
                        foreach (var x in _memberValues)
                        {
                            if (!x.Value) continue;
                            result = true;
                            break;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            else
                throw new CompositeException("Could not determine composite value. " +
                                             "Failed to calculate composite result.");

            return result;
        }

        private bool ValidateComposite()
        {
            //If variable not set to composite throw an exception
            if (!_composite)
                throw new CompositeException($"Could not determine composite value. " +
                                             $"Variable is not set to composite");
            //Check if null member values exist in list
            _memberValues.RemoveAll(x => x == null);
            //Allow only unique member values
            _memberValues = _memberValues.Distinct().ToList();
            //If there are no member values throw an exception
            if (_memberValues.Count <= 0)
                throw new CompositeException("Could not determine composite value. " +
                                             "Composite does not have any member values.");
            //Set all member values to global scope
            if (_memberValues.Any(x => x.Scope != Scope.Global))
            {
                foreach (var reference in _memberValues.Where(x => x.Scope == Scope.Local)) reference.Scope = Scope.Global;
                throw new CompositeException("Could not determine composite value. " +
                                             "Member values can only be of global scope. " +
                                             "Member values have now been set to global scope.");
            }

            if (_memberValues.Any(x => x.GlobalValue == null))
                throw new CompositeException("Could not determine composite value. " +
                                             "There are member values without a reference to a variable.");
            return true;
        }

        #endregion

        #region Events

        #region Invocation

        protected override void InvokeOnValueChangedEvents(bool value, bool prevValue)
        {
            if (!_composite)
            {
                base.InvokeOnValueChangedEvents(value, prevValue);
                if (prevValue == value) return;
                if (value) InvokeOnValueChangedToTrueEvent();
                else InvokeOnValueChangedToFalseEvent();
            }
            else
                //TODO
            {
                throw new NotImplementedException();
            }
        }

        public void InvokeOnValueChangedToFalseEvent()
        {
            _onValueChangedToFalseEvent.Invoke(false);
        }

        public void InvokeOnValueChangedToTrueEvent()
        {
            _onValueChangedToTrueEvent.Invoke(true);
        }

        #endregion

        #region Listeners

        public void AddListenerToOnValueChangedToTrueEvent(UnityAction<bool> action)
        {
            _onValueChangedToTrueEvent.AddListener(action);
        }

        public void AddListenerToOnValueChangedToFalseEvent(UnityAction<bool> action)
        {
            _onValueChangedToFalseEvent.AddListener(action);
        }

        public void RemoveListenerFromOnValueChangedToTrueEvent(UnityAction<bool> action)
        {
            _onValueChangedToTrueEvent.RemoveListener(action);
        }

        public void RemoveListenerFromOnChangedToFalseEvent(UnityAction<bool> action)
        {
            _onValueChangedToFalseEvent.RemoveListener(action);
        }

        public void AddListenerToOnAfterDeserializedEvent(UnityAction action)
        {
            _onAfterDeserializedEvent.AddListener(action);
        }

        public void RemoveListenerFromOnAfterDeserializedEvent(UnityAction action)
        {
            _onAfterDeserializedEvent.RemoveListener(action);
        }

        #endregion

        #endregion

        /// <summary>
        /// If composite register this as a use for the composite member values
        /// </summary>
        public void Register()
        {
            if (!_composite) return;
            foreach (var memberValue in _memberValues)
                if (memberValue?.Scope == Scope.Global)
                    memberValue?.GlobalValue?.AddUse(this, memberValue);
        }
    }

    [Serializable]
    public class CompositeException : Exception
    {
        public CompositeException(string message) : base(message)
        {
        }
    }
}