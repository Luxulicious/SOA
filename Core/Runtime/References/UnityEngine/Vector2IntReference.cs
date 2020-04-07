using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector2IntReference : Reference<Vector2IntVariable, Vector2Int, Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent>
    {
        public Vector2IntReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Vector2IntReference(IRegisteredReferenceContainer registration, Vector2Int value) : base(registration, value)
        {
        }
    }
}