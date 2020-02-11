using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Vector4 Variable", menuName = "SOA/UnityEngine/Vector4/Variable", order = 1)]
    public class Vector4Variable : Variable<Vector4, Vector4UnityEvent, Vector4Vector4UnityEvent>
    {
    }
}