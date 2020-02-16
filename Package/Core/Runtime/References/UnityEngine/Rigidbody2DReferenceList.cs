using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Rigidbody2DReferenceList : ReferenceList<Rigidbody2DReference, Rigidbody2DVariable, Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent
        , Rigidbody2DReferenceUnityEvent>
    {
    }
}