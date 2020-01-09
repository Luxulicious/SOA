using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(IntGameEvent))]
    public class IntGameEventEditor : UnityEventSOEditor<IntGameEvent, IntUnityEvent, int>
    {
    }
}