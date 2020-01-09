using SOA.Base;
using UnityEditor;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class
        BoolReferenceDrawer : ReferenceDrawer<BoolReference, BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
    }
}