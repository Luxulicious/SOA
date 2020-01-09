using SOA.Base;
using UnityEditor;
using System;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(ByteGameEvent))]
    public class ByteGameEventEditor : UnityEventSOEditor<ByteGameEvent, ByteUnityEvent, byte>
    {
    }
}