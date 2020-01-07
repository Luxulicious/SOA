using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(QuaternionGameEvent))]
    public class QuaternionGameEventEditor : UnityEventSOEditor<QuaternionGameEvent, QuaternionUnityEvent, Quaternion>
    {
    }
}