using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New String Event", menuName = "SOA/Primitives/String/Event", order = 1)]
    public class StringGameEvent : GameEvent<StringUnityEvent, string>
    {
    }
}