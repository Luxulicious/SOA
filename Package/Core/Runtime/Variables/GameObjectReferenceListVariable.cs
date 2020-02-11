using SOA.Base;
using UnityEngine;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CreateAssetMenu(fileName = "New GameObject List", menuName = "SOA/GameObject/List", order = 1)]
    public class GameObjectReferenceListVariable : ReferenceListVariable<GameObjectReferenceList, GameObjectReference, GameObjectVariable, GameObject,
        GameObjectUnityEvent, GameObjectGameObjectUnityEvent, GameObjectReferenceUnityEvent>
    {
    }
}