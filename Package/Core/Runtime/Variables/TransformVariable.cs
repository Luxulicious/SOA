using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Transform Variable", menuName = "SOA/UnityEngine/Components/Transform/Variable", order = 1)]
    public class TransformVariable : Variable<Transform, TransformUnityEvent, TransformTransformUnityEvent>
    {
    }
}