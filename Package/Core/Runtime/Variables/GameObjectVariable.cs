using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New GameObject Variable", menuName = "SOA/GameObject/Variable", order = 1)]
    public class GameObjectVariable : Variable<GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent>
    {
    }
}