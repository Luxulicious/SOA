using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody Variable", menuName = "SOA/UnityEngine/Components/Rigidbody/Variable", order = 1)]
    public class RigidbodyVariable : Variable<Rigidbody, RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent>
    {
    }
}