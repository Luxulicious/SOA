using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TouchUnityEvent : UnityEvent<Touch>
    {
    }

    [Serializable]
    public class TouchTouchUnityEvent : UnityEvent<Touch, Touch>
    {
    }

    [Serializable]
    public class TouchReferenceUnityEvent : UnityEvent<TouchReference>
    {
    }
}