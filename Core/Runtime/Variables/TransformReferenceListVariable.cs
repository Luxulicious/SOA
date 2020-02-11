using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Transform List", menuName = "SOA/UnityEngine/Components/Transform/List", order = 1)]
    public class TransformReferenceListVariable : ReferenceListVariable<TransformReferenceList, TransformReference, TransformVariable, Transform,
        TransformUnityEvent, TransformTransformUnityEvent, TransformReferenceUnityEvent>
    {
    }
}