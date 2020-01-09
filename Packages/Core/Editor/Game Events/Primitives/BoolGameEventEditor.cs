using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(BoolGameEvent))]
    public class BoolGameEventEditor : UnityEventSOEditor<BoolGameEvent, BoolUnityEvent, bool>
    {
    }
}