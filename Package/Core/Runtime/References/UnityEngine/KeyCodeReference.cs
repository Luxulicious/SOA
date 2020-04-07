using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

//TODO Move to seperate file
public interface IInput
{
    bool Active { get; }
}

//TODO Move to seperate file
public interface IDigitalInput : IInput
{
}

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class KeyCodeReference : Reference<KeyCodeVariable, KeyCode, KeyCodeUnityEvent, KeyCodeKeyCodeUnityEvent>,
        IDigitalInput

    {
        public bool Active => Input.GetKey(Value);

        public KeyCodeReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public KeyCodeReference(IRegisteredReferenceContainer registration, KeyCode value) : base(registration, value)
        {
        }
    }
}