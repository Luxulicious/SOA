using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class GameObjectReference : Reference<GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent>
    {
    }
}