using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(BoolGameEvent))]
    public class BoolGameEventEditor : GameEventEditor<BoolGameEvent, BoolUnityEvent, bool>
    {
    }
}