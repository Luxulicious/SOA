using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Quaternion List", menuName = "SOA/UnityEngine/Quaternion/List", order = 1)]
    public class QuaternionReferenceListVariable : ReferenceListVariable<QuaternionReferenceList, QuaternionReference, QuaternionVariable, Quaternion,
        QuaternionUnityEvent, QuaternionQuaternionUnityEvent, QuaternionReferenceUnityEvent>
    {
    }
}