using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomEditor(typeof(GameObjectVariable))]
    public class GameObjectVariableEditor : VariableEditor<GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent>
    {

    }
}