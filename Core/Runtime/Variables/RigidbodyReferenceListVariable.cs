using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody List", menuName = "SOA/UnityEngine/Components/Rigidbody/List", order = 1)]
    public class RigidbodyReferenceListVariable : ReferenceListVariable<RigidbodyReferenceList, RigidbodyReference, RigidbodyVariable, Rigidbody,
        RigidbodyUnityEvent, RigidbodyRigidbodyUnityEvent, RigidbodyReferenceUnityEvent>
    {
    }
}