using System;
using System.Collections.Generic;
using System.Linq;
using SOA.Base;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Bool Variable", menuName = "SOA/Primitives/Bool/Variable", order = 1)]
    public class BoolVariable : Variable<bool, BoolUnityEvent, BoolBoolUnityEvent>, ISerializationCallbackReceiver
    {
        [SerializeField] private BoolUnityEvent _onValueChangedToTrueEvent = new BoolUnityEvent();
        [SerializeField] private BoolUnityEvent _onValueChangedToFalseEvent = new BoolUnityEvent();

        [SerializeField] [Tooltip("Value will be based on a composite of one or more referenced variables")]
        private bool _composite = false;

        [SerializeField] private BooleanOperator _andOr;
        [SerializeField] private List<BoolReference> _memberValues = new List<BoolReference>(1);

        public override bool Value
        {
            get
            {
                if (_composite) UpdateComposite();

                return base.Value;
            }
            set
            {
                if (_composite)
                {
                    UpdateComposite();
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
                        $"The default value of {GetType().Name} is not used in a static context.");
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
                    Debug.LogWarning($"{typeof(Persistence).Name} of type {GetType().Name} is always variable");
                    if (_persistence == Persistence.Constant)
                        _persistence = Persistence.Variable;
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
                        Debug.LogWarning($"{typeof(Persistence).Name} of type {GetType().Name} is always variable");
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

        public void UpdateComposite()
        {
            if (_composite)
            {
                if (_memberValues.Count > 0)
                {
                    bool result;
                    switch (_andOr)
                    {
                        case BooleanOperator.And:
                            result = true;
                            foreach (var x in _memberValues)
                                if (!x.Value)
                                {
                                    result = false;
                                    break;
                                }

                            break;
                        case BooleanOperator.Or:
                            result = false;
                            foreach (var x in _memberValues)
                                if (x.Value)
                                {
                                    result = true;
                                    break;
                                }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    base.Value = result;
                }
                else
                {
                    throw new CompositeException(
                        $"At least 1 member value needs to exist when using composite");
                }
            }
            else
            {
                throw new CompositeException(
                    $"Value is not set to composite");
            }
        }

        protected override void InvokeOnChangeEvents(bool prevValue, bool value)
        {
            base.InvokeOnChangeEvents(prevValue, value);
            if (prevValue == value) return;
            if (value) InvokeOnChangedToTrueEvent();
            else InvokeOnChangedToFalseEvent();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (!_composite) return;
            try
            {
                UpdateComposite();
            }
            catch (CompositeException e)
            {
                Debug.LogWarning(e.Message);
            }

            SetAllReferencesToGlobalScope();
        }

        private void SetAllReferencesToGlobalScope()
        {
            if (!_memberValues.Any()) return;
            foreach (var reference in _memberValues.Where(x => x.Scope == Scope.Local)) reference.Scope = Scope.Global;
        }

        public void InvokeOnChangedToFalseEvent()
        {
            _onValueChangedToFalseEvent.Invoke(false);
        }

        public void InvokeOnChangedToTrueEvent()
        {
            _onValueChangedToTrueEvent.Invoke(true);
        }

        public void AddAutoListener(UnityAction<bool> onChangeListener,
            UnityAction<bool, bool> onChangeWithHistoryListener, UnityAction<bool> onValueChangedToTrueEventListener,
            UnityAction<bool> onValueChangedToFalseEventListener)
        {
            base.AddAutoListener(onChangeListener, onChangeWithHistoryListener);
            _onValueChangedToTrueEvent.AddListener(onValueChangedToTrueEventListener);
            _onValueChangedToFalseEvent.AddListener(onValueChangedToFalseEventListener);
        }

        public void RemoveAutoListener(UnityAction<bool> onChangeListener,
            UnityAction<bool, bool> onChangeWithHistoryListener, UnityAction<bool> onValueChangedToTrueEventListener,
            UnityAction<bool> onValueChangedToFalseEventListener)
        {
            base.RemoveAutoSubscriber(onChangeListener, onChangeWithHistoryListener);
            _onValueChangedToTrueEvent.RemoveListener(onValueChangedToTrueEventListener);
            _onValueChangedToFalseEvent.RemoveListener(onValueChangedToFalseEventListener);
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