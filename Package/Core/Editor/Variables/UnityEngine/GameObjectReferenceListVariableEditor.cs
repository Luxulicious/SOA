using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{

    [CustomEditor(typeof(GameObjectReferenceListVariable))]
    public class GameObjectReferenceListVariableEditor : VariableReferenceListEditor<GameObjectReferenceListVariable,
        GameObjectReferenceList,
        GameObjectReference, GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent, GameObjectReferenceUnityEvent>
    {
    }
}