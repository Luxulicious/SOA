using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody2D Variable", menuName = "SOA/UnityEngine/Components/Rigidbody2D/Variable", order = 1)]
    public class Rigidbody2DVariable : Variable<Rigidbody2D, Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent>
    {
    }
}