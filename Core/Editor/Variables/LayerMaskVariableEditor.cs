using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(LayerMaskVariable))]
    public class LayerMaskVariableEditor : VariableEditor<LayerMaskVariable, LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent>
    {

    }
}