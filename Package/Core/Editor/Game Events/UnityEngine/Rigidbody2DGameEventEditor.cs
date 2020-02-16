using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomEditor(typeof(Rigidbody2DGameEvent))]
    public class Rigidbody2DGameEventEditor : GameEventEditor<Rigidbody2DGameEvent, Rigidbody2DUnityEvent, Rigidbody2D>
    {
    }
}