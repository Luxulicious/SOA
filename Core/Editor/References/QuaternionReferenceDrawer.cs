using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(QuaternionReference))]
    public class
        QuaternionReferenceDrawer : ReferenceDrawer<QuaternionReference, QuaternionVariable, Quaternion, QuaternionUnityEvent, QuaternionQuaternionUnityEvent>
    {
    }
}