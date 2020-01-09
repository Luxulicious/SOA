using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector3Int List", menuName = "SOA/UnityEngine/Vector3Int/List", order = 1)]
    public class Vector3IntReferenceListVariable : ReferenceListVariable<Vector3IntReferenceList, Vector3IntReference, Vector3IntVariable, Vector3Int,
        Vector3IntUnityEvent, Vector3IntVector3IntUnityEvent, Vector3IntReferenceUnityEvent>
    {
    }
}