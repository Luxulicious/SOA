using System;
using SOA.Base;
using UnityEngine;

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
    }
}