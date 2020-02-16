using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Rigidbody2D List", menuName = "SOA/UnityEngine/Components/Rigidbody2D/List", order = 1)]
    public class Rigidbody2DReferenceListVariable : ReferenceListVariable<Rigidbody2DReferenceList, Rigidbody2DReference, Rigidbody2DVariable, Rigidbody2D,
        Rigidbody2DUnityEvent, Rigidbody2DRigidbody2DUnityEvent, Rigidbody2DReferenceUnityEvent>
    {
    }
}