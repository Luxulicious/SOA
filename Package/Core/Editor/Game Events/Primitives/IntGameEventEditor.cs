using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(IntGameEvent))]
    public class IntGameEventEditor : GameEventEditor<IntGameEvent, IntUnityEvent, int>
    {
    }
}