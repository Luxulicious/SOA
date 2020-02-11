using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class LayerMaskGameEventListener : GameEventListener<LayerMaskGameEvent, LayerMaskUnityEvent, LayerMask>
    {
    }
}