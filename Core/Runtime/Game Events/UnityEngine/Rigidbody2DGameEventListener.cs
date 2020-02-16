using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Rigidbody2DGameEventListener : GameEventListener<Rigidbody2DGameEvent, Rigidbody2DUnityEvent, Rigidbody2D>
    {
    }
}