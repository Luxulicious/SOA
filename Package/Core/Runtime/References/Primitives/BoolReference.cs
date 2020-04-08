using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

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
            if(!_invertResult)
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
            //Remove existing listeners from previous global value
            _prevGlobalValue?.RemoveListenerFromOnChangeToTrueEvent(InvokeOnValueChangedToTrueEvent);
            _prevGlobalValue?.RemoveListenerFromOnChangeToFalseEvent(InvokeOnValueChangedToFalseEvent);
            //Remove existing listeners from current global value
            _globalValue?.RemoveListenerFromOnChangeToTrueEvent(InvokeOnValueChangedToTrueEvent);
            _globalValue?.RemoveListenerFromOnChangeToFalseEvent(InvokeOnValueChangedToFalseEvent);
            //Add listeners to current global Value
            _globalValue?.AddListenerToOnChangeToTrueEvent(InvokeOnValueChangedToTrueEvent);
            _globalValue?.AddListenerToOnChangeToFalseEvent(InvokeOnValueChangedToFalseEvent);
        }


    }
}