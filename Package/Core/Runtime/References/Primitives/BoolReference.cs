using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolReference : Reference<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>, ISerializationCallbackReceiver
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

        public override bool Value
        {
            get => _invertResult ? !base.Value : base.Value;
            set => base.Value = value;
        }

        protected override void InvokeOnChangeResponses(bool currentValue)
        {
            if (!_invertResult)
                base.InvokeOnChangeResponses(currentValue);
            else
                base.InvokeOnChangeResponses(!currentValue);
        }

        protected override void InvokeOnValueChangeWithHistoryResponses(bool currentValue, bool previousValue)
        {
            if (!_invertResult)
                base.InvokeOnValueChangeWithHistoryResponses(currentValue, previousValue);
            else
                base.InvokeOnValueChangeWithHistoryResponses(!currentValue, !previousValue);
        }

        protected void InvokeOnValueChangedToTrueResponses(bool value)
        {
            if(!_invertResult)
                _onValueChangedToTrueEvent.Invoke(true);
            else
                _onValueChangedToFalseEvent.Invoke(false);
        }

        protected void InvokeOnValueChangedToFalseResponses(bool value)
        {
            if (!_invertResult)
                _onValueChangedToFalseEvent.Invoke(false);
            else
                _onValueChangedToTrueEvent.Invoke(true);
        }

        public override void OnAfterDeserialize()
        {
            _prevGlobalValue?.RemoveAutoListener(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses, InvokeOnValueChangedToTrueResponses, InvokeOnValueChangedToFalseResponses);
            _globalValue?.RemoveAutoListener(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses, InvokeOnValueChangedToTrueResponses, InvokeOnValueChangedToFalseResponses);
            _globalValue?.AddAutoListener(InvokeOnChangeResponses, InvokeOnValueChangeWithHistoryResponses, InvokeOnValueChangedToTrueResponses, InvokeOnValueChangedToFalseResponses);
            _prevGlobalValue = _globalValue;
        }
    }
}