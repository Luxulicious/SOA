using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3 List", menuName = "SOA/UnityEngine/Vector3/List", order = 1)]
    public class Vector3ReferenceListVariable : ReferenceListVariable<Vector3ReferenceList, Vector3Reference, Vector3Variable, Vector3,
        Vector3UnityEvent, Vector3Vector3UnityEvent, Vector3ReferenceUnityEvent>
    {
    }
}