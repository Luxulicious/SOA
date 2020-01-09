using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector2IntUnityEvent : UnityEvent<Vector2Int>
    {
    }

    [Serializable]
    public class Vector2IntVector2IntUnityEvent : UnityEvent<Vector2Int, Vector2Int>
    {
    }

    [Serializable]
    public class Vector2IntReferenceUnityEvent : UnityEvent<Vector2IntReference>
    {
    }
}