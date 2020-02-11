using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.UnityEngine
{
    [CustomPropertyDrawer(typeof(Color32Reference))]
    public class
        Color32ReferenceDrawer : ReferenceDrawer<Color32Reference, Color32Variable, Color32, Color32UnityEvent, Color32Color32UnityEvent>
    {
    }
}