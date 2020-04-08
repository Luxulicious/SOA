using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class LayerMaskReference : Reference<LayerMaskVariable, LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent>
    {
        public LayerMaskReference()
        {
        }

        public LayerMaskReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public LayerMaskReference(IRegisteredReferenceContainer registration, LayerMask value) : base(registration, value)
        {
        }
    }
}