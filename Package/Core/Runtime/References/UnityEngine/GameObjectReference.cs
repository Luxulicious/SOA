using System;
using SOA.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SOA.Common.CustomTypes
{
    [Serializable]
    public class GameObjectReference : Reference<GameObjectVariable, GameObject, GameObjectUnityEvent, GameObjectGameObjectUnityEvent>
    {
        public GameObjectReference(IRegisteredReferenceContainer registration) : base(registration)
        {
        }

        public GameObjectReference(IRegisteredReferenceContainer registration, GameObject value) : base(registration, value)
        {
        }
    }
}