using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New LayerMask Event", menuName = "SOA/UnityEngine/LayerMask/Event", order = 1)]
    public class LayerMaskGameEvent : GameEvent<LayerMaskUnityEvent, LayerMask>
    {
    }
}