using System;
using UnityEngine.Events;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [Serializable]
    public class Vector3IntUnityEvent : UnityEvent<Vector3Int>
    {
    }

    [Serializable]
    public class Vector3IntVector3IntUnityEvent : UnityEvent<Vector3Int, Vector3Int>
    {
    }

    [Serializable]
    public class Vector3IntReferenceUnityEvent : UnityEvent<Vector3IntReference>
    {
    }
}