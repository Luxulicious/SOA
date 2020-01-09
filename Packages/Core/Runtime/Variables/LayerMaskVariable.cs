using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New LayerMask Variable", menuName = "SOA/UnityEngine/LayerMask/Variable", order = 1)]
    public class LayerMaskVariable : Variable<LayerMask, LayerMaskUnityEvent, LayerMaskLayerMaskUnityEvent>
    {
    }
}