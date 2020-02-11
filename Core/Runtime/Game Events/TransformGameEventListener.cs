using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class TransformGameEventListener : GameEventListener<TransformGameEvent, TransformUnityEvent, Transform>
    {
    }
}