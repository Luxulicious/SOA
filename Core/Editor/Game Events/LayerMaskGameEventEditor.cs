using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(LayerMaskGameEvent))]
    public class LayerMaskGameEventEditor : UnityEventSOEditor<LayerMaskGameEvent, LayerMaskUnityEvent, LayerMask>
    {
    }
}