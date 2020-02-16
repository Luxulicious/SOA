using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class RigidbodyReferenceList : ReferenceList<RigidbodyReference, RigidbodyVariable, Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent
        , RigidbodyReferenceUnityEvent>
    {
    }
}