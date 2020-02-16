using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New LayerMask List", menuName = "SOA/UnityEngine/LayerMask/List", order = 1)]
    public class LayerMaskReferenceListVariable : ReferenceListVariable<LayerMaskReferenceList, LayerMaskReference, LayerMaskVariable, LayerMask,
        LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent, LayerMaskReferenceUnityEvent>
    {
    }
}