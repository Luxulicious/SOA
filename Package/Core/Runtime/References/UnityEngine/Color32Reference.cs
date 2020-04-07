using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Color32Reference : Reference<Color32Variable, Color32, Color32UnityEvent, Color32Color32UnityEvent>
    {
        public Color32Reference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Color32Reference(IRegisteredReferenceContainer registration, Color32 value) : base(registration, value)
        {
        }
    }
}