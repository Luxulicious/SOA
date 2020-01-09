using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Char Event", menuName = "SOA/Primitives/Char/Event", order = 1)]
    public class CharGameEvent : GameEvent<CharUnityEvent, char>
    {
    }
}