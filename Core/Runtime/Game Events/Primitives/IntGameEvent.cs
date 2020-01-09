using System.Collections;
using System.Collections.Generic;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "SOA/Primitives/Int/Event", order = 1)]
    public class IntGameEvent : GameEvent<IntUnityEvent, int>
    {

    }
}