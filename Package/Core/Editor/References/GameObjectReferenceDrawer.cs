using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [CustomPropertyDrawer(typeof(GameObjectReference))]
    public class
        GameObjectReferenceDrawer : ReferenceDrawer<GameObjectReference, GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent>
    {
    }
}