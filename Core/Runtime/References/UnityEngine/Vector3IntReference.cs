using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector3IntReference : Reference<Vector3IntVariable, Vector3Int, Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent>
    {
        public Vector3IntReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public Vector3IntReference(IRegisteredReferenceContainer registration, Vector3Int value) : base(registration, value)
        {
        }
    }
}