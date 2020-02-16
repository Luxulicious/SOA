using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class GameObjectUnityEvent : UnityEvent<GameObject>
    {
    }

    [Serializable]
    public class GameObjectGameObjectUnityEvent : UnityEvent<GameObject, GameObject>
    {
    }

    [Serializable]
    public class GameObjectReferenceUnityEvent : UnityEvent<GameObjectReference>
    {
    }
}