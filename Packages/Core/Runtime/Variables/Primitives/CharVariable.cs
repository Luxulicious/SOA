using SOA.Base;
using UnityEngine;
using System;

namespace SOA.Common.Primitives
{
    [CreateAssetMenu(fileName = "New Char Variable", menuName = "SOA/Primitives/Char/Variable", order = 1)]
    public class CharVariable : Variable<char, CharUnityEvent, CharCharUnityEvent>
    {
    }
}