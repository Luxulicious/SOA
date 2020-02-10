using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class GameObjectReferenceList : ReferenceList<GameObjectReference, GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent
        , GameObjectReferenceUnityEvent>
    {
    }
}