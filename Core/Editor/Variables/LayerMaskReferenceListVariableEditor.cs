using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{

    [CustomEditor(typeof(LayerMaskReferenceListVariable))]
    public class LayerMaskReferenceListVariableEditor : VariableReferenceListEditor<LayerMaskReferenceListVariable,
        LayerMaskReferenceList,
        LayerMaskReference, LayerMaskVariable, LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent, LayerMaskReferenceUnityEvent>
    {
    }
}