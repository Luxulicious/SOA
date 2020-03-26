using System.Collections.Generic;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [ExecuteAlways]
    public class BoolReferenceComponent : ReferenceComponent<BoolReference, BoolVariable, bool, BoolUnityEvent,
        BoolBoolUnityEvent>, ISerializationCallbackReceiver
    {
        
        [SerializeField] private BoolUnityEvent _onValueChangedToTrueEvent = new BoolUnityEvent();
        [SerializeField] private BoolUnityEvent _onValueChangedToFalseEvent = new BoolUnityEvent();
        

        public void OnAfterDeserialize()
        {
            _reference.RemoveListener(InvokeOnValueChangedToEvents);
        }

        public void OnBeforeSerialize()
        {
            _reference.AddListener(InvokeOnValueChangedToEvents);
        }

        private void InvokeOnValueChangedToEvents(bool value)
        {
            if (value) _onValueChangedToTrueEvent.Invoke(value);
            else _onValueChangedToFalseEvent.Invoke(value);
        }
    }
}