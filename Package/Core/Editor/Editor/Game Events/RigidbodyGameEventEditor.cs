using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(RigidbodyGameEvent))]
    public class RigidbodyGameEventEditor : GameEventEditor<RigidbodyGameEvent, RigidbodyUnityEvent, Rigidbody>
    {
    }
}