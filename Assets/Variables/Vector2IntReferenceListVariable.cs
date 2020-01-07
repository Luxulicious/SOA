using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2Int List", menuName = "SOA/UnityEngine/Vector2Int/List", order = 1)]
    public class Vector2IntReferenceListVariable : ReferenceListVariable<Vector2IntReferenceList, Vector2IntReference, Vector2IntVariable, Vector2Int,
        Vector2IntUnityEvent, Vector2IntVector2IntUnityEvent, Vector2IntReferenceUnityEvent>
    {
    }
}