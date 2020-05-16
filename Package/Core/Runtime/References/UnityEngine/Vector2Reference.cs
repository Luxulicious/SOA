using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector2Reference : Reference<Vector2Variable, Vector2, Vector2UnityEvent, Vector2Vector2UnityEvent>
    {
        public Vector2Reference()
        {
        }

        public Vector2Reference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Vector2Reference(IRegisteredReferenceContainer registration, Vector2 value) : base(registration, value)
        {
        }

        public Vector2Reference(Vector2 value) : base(value)
        {
        }
    }
}