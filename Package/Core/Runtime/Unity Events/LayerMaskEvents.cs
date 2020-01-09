using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class LayerMaskUnityEvent : UnityEvent<LayerMask>
    {
    }

    [Serializable]
    public class LayerMaskLayerMaskUnityEvent : UnityEvent<LayerMask, LayerMask>
    {
    }

    [Serializable]
    public class LayerMaskReferenceUnityEvent : UnityEvent<LayerMaskReference>
    {
    }
}