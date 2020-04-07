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
            if (!HasRegistration())
                Debug.LogWarning(
                    $"No registration found for {typeof(Reference).Name} {this}. \n" +
                    $"Please register when instancing a reference. \n" +
                    $"This can be done manually or by using {typeof(RegisteredMonoBehaviour).Name} instead of {typeof(MonoBehaviour).Name}."
                    , _globalValue);
        }
    }
}