using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(LayerMaskGameEvent))]
    public class LayerMaskGameEventEditor : GameEventEditor<LayerMaskGameEvent, LayerMaskUnityEvent, LayerMask>
    {
    }
}