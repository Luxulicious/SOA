using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CreateAssetMenu(fileName = "New Quaternion Variable", menuName = "SOA/UnityEngine/Quaternion/Variable", order = 1)]
    public class QuaternionVariable : Variable<Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent>
    {
    }
}