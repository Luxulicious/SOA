using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [ExecuteAlways]
    public class BoolReferenceComponent : ReferenceComponent<BoolReference, BoolVariable, bool, BoolUnityEvent,
        BoolBoolUnityEvent>, ISerializationCallbackReceiver
    {
        //TODO This type of bool referencing should be done ideally from bool reference itself and not its component
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