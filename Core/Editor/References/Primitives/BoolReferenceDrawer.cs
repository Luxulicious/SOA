using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class
        BoolReferenceDrawer : ReferenceDrawer<BoolReference, BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
        //TODO Get this dynamically
        private static readonly string _invertPropertyPath = "_invertResult";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var invertProperty = property.FindPropertyRelative(_invertPropertyPath);

            return base.GetPropertyHeight(property, label) + EditorGUI.GetPropertyHeight(invertProperty);
        }

        protected override void PostOnInspectorGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var basePropertyHeight = base.GetPropertyHeight(property, label);
            var invertProperty = property.FindPropertyRelative(_invertPropertyPath);
            EditorGUI.PropertyField(
                new Rect(position.x, (position.y + basePropertyHeight + _breakLine), position.width,
                    EditorGUI.GetPropertyHeight(invertProperty)), invertProperty, true);
        }

    }
}