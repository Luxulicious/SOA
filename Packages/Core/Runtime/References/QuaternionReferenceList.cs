using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class QuaternionReferenceList : ReferenceList<QuaternionReference, QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent
        , QuaternionReferenceUnityEvent>
    {
    }
}