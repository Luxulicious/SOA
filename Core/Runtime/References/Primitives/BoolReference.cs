using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolReference : Reference<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
        [SerializeField] private BoolUnityEvent _onValueChangedToTrueEvent = new BoolUnityEvent();
        [SerializeField] private BoolUnityEvent _onValueChangedToFalseEvent = new BoolUnityEvent();

        [SerializeField, Tooltip("Inverts the result when trying to GET the value;  " +
                                 "\nthe value(s) received from On Change Event & On Change Event With History;" +
                                 "\ninverts the invocation for On Change To True/False Events accordingly." +
                                 "\nNOTE: SETting the value is completely unaffected."
         )]
        //TODO Make this collapsible
        private bool _invertResult = false;

        public BoolReference()
        {
        }

        public BoolReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public BoolReference(IRegisteredReferenceContainer registration, bool value) : base(registration, value)
        {
        }

        public BoolReference(bool value) : base(value)
        {
        }

        public override bool Value
        {
            get => _invertResult ? !base.Value : base.Value;
            set => base.Value = value;
        }

        protected override void InvokeOnValueChanged(bool currentValue)
        {
            if (!_invertResult)
                base.InvokeOnValueChanged(currentValue);
            else
                base.InvokeOnValueChanged(!currentValue);
        }

        protected override void InvokeOnValueChangedWithHistory(bool currentValue, bool previousValue)
        {
            if (!_invertResult)
                base.InvokeOnValueChangedWithHistory(currentValue, previousValue);
            else
                base.InvokeOnValueChangedWithHistory(!currentValue, !previousValue);
        }

        protected void InvokeOnValueChangedToTrueEvent(bool value)
        {
            if (!_invertResult)
                _onValueChangedToTrueEvent.Invoke(true);
            else
                _onValueChangedToFalseEvent.Invoke(false);
        }

        protected void InvokeOnValueChangedToFalseEvent(bool value)
        {
            if (!_invertResult)
                _onValueChangedToFalseEvent.Invoke(false);
            else
                _onValueChangedToTrueEvent.Invoke(true);
        }

        public override void RefreshRegistrationsToGlobalValue()
        {
            base.RefreshRegistrationsToGlobalValue();
        }

        public override void RefreshListenersToGlobalValueOnValueChangedEvents()
        {
            using (refreshListenersToGlobalValueOnValueChangedEventsMarker.Auto())
            {
                if (CanRefreshListenersToGlobalValueOnValueChangedEvents())
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
                    //Remove existing listeners from previous global value
                    _prevGlobalValue?.RemoveListenerFromOnValueChangedToTrueEvent(InvokeOnValueChangedToTrueEvent);
                    _prevGlobalValue?.RemoveListenerFromOnChangedToFalseEvent(InvokeOnValueChangedToFalseEvent);
                    //Remove existing listeners from current global value
                    _globalValue?.RemoveListenerFromOnValueChangedToTrueEvent(InvokeOnValueChangedToTrueEvent);
                    _globalValue?.RemoveListenerFromOnChangedToFalseEvent(InvokeOnValueChangedToFalseEvent);
                    //Add listeners to current global Value
                    _globalValue?.AddListenerToOnValueChangedToTrueEvent(InvokeOnValueChangedToTrueEvent);
                    _globalValue?.AddListenerToOnValueChangedToFalseEvent(InvokeOnValueChangedToFalseEvent);
                    //Refresh prevGlobalValue
                    _prevGlobalValue = _globalValue;
                }
            }
        }

        public override bool CanRefreshListenersToGlobalValueOnValueChangedEvents()
        {
            //TODO Maybe change to if _prevGlobalValue != _globalValue && !_composite|| _prevCompositeValue != _compositeValue && _composite
            return true;
        }
    }
}