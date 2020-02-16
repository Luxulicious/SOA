using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TouchReferenceList : ReferenceList<TouchReference, TouchVariable, Touch, TouchUnityEvent, TouchTouchUnityEvent
        , TouchReferenceUnityEvent>
    {
    }
}