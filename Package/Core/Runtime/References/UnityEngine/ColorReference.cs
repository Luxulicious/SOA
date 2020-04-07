using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class ColorReference : Reference<ColorVariable, Color, ColorUnityEvent, ColorColorUnityEvent>
    {
        public ColorReference(IRegisteredReferenceContainer registration) : base(registration)
        {

        }

        public ColorReference(IRegisteredReferenceContainer registration, Color value) : base(registration, value)
        {
        }
    }
}