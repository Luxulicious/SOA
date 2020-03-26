using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [Serializable]
    public class BoolReference : Reference<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
        [SerializeField, Tooltip("Inverts the result when trying to GET the value " +
                                 "\nand the value(s) received from On Change Events." +
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
    }
}