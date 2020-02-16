using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector4 List", menuName = "SOA/UnityEngine/Vector4/List", order = 1)]
    public class Vector4ReferenceListVariable : ReferenceListVariable<Vector4ReferenceList, Vector4Reference, Vector4Variable, Vector4,
        Vector4UnityEvent, Vector4Vector4UnityEvent, Vector4ReferenceUnityEvent>
    {
    }
}