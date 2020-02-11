using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(TransformGameEvent))]
    public class TransformGameEventEditor : GameEventEditor<TransformGameEvent, TransformUnityEvent, Transform>
    {
    }
}