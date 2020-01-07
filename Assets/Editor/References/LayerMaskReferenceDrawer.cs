using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(LayerMaskReference))]
    public class
        LayerMaskReferenceDrawer : ReferenceDrawer<LayerMaskReference, LayerMaskVariable, LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent>
    {
    }
}