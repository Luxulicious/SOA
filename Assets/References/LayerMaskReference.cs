using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class LayerMaskReference : Reference<LayerMaskVariable, LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent>
    {
    }
}