using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector2 List", menuName = "SOA/UnityEngine/Vector2/List", order = 1)]
    public class Vector2ReferenceListVariable : ReferenceListVariable<Vector2ReferenceList, Vector2Reference, Vector2Variable, Vector2,
        Vector2UnityEvent, Vector2Vector2UnityEvent, Vector2ReferenceUnityEvent>
    {
    }
}