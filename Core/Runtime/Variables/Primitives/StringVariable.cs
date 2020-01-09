using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New String Variable", menuName = "SOA/Primitives/String/Variable", order = 1)]
    public class StringVariable : Variable<string, StringUnityEvent, StringStringUnityEvent>
    {
    }
}