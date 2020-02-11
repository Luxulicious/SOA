using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class RigidbodyGameEventListener : GameEventListener<RigidbodyGameEvent, RigidbodyUnityEvent, Rigidbody>
    {
    }
}