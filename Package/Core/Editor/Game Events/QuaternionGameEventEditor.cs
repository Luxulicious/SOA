using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(QuaternionGameEvent))]
    public class QuaternionGameEventEditor : GameEventEditor<QuaternionGameEvent, QuaternionUnityEvent, Quaternion>
    {
    }
}