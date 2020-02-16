using System;
using SOA.Base;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class GameObjectGameEventListener : GameEventListener<GameObjectGameEvent, GameObjectUnityEvent, GameObject>
    {
    }
}