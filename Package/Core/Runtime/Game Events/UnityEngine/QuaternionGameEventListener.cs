using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class QuaternionGameEventListener : GameEventListener<QuaternionGameEvent, QuaternionUnityEvent, Quaternion>
    {
    }
}