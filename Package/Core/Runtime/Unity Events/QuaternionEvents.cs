using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class QuaternionUnityEvent : UnityEvent<Quaternion>
    {
    }

    [Serializable]
    public class QuaternionQuaternionUnityEvent : UnityEvent<Quaternion, Quaternion>
    {
    }

    [Serializable]
    public class QuaternionReferenceUnityEvent : UnityEvent<QuaternionReference>
    {
    }
}