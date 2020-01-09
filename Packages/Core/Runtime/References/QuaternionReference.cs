using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class QuaternionReference : Reference<QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent>
    {
    }
}