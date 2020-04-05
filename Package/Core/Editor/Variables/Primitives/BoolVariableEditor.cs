using SOA.Base;
using UnityEditor;
using UnityEngine;

namespace SOA.Common.Primitives
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : VariableEditor<BoolVariable, bool, BoolUnityEvent, BoolBoolUnityEvent>
    {
        //TODO Dynamically determine these string via member information of the class
        protected static readonly string _compositePropertyPath = "_composite";
        protected static readonly string _onValueChangedToTrueEventPropertyPath = "_onValueChangedToTrueEvent";
        protected static readonly string _onValueChangedToFalseEventPropertyPath = "_onValueChangedToFalseEvent";
        protected static readonly string _andOrPropertyPath = "_andOr";
        protected static readonly string _memberValuesPropertyPath = "_memberValues";

        protected override void DrawPersistencePropertyField(SerializedProperty useAsConstantProperty)
        {
            var compositeProperty = serializedObject.FindProperty(_compositePropertyPath);
            var compositeValue = compositeProperty.boolValue;
            if (!compositeValue)
                base.DrawPersistencePropertyField(useAsConstantProperty);
        }

        protected override void DrawDefaultValuePropertyField(SerializedProperty defaultValueProperty,
            string label = null)
        {
            var compositeProperty = serializedObject.FindProperty(_compositePropertyPath);
            var compositeValue = compositeProperty.boolValue;
            if (!compositeValue)
                base.DrawDefaultValuePropertyField(defaultValueProperty, label);
        }

        protected override void DrawInvokeOnChangeEventsButtons(BoolVariable variable)
        {
            base.DrawInvokeOnChangeEventsButtons(variable);
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            if (GUILayout.Button("Invoke On Changed To True Event")) variable.InvokeOnChangedToTrueEvent();
            if (GUILayout.Button("Invoke On Changed To False Event")) variable.InvokeOnChangedToFalseEvent();
            EditorGUI.EndDisabledGroup();
        }

        protected override void DrawOnChangeEventsPropertyFields(SerializedProperty onChangedEventProperty,
            SerializedProperty onChangedWithHistoryEventProperty)
        {
            base.DrawOnChangeEventsPropertyFields(onChangedEventProperty, onChangedWithHistoryEventProperty);
            var onValueChangedToTrueEventProperty =
                serializedObject.FindProperty(_onValueChangedToTrueEventPropertyPath);
            EditorGUILayout.PropertyField(onValueChangedToTrueEventProperty, true);
            var onValueChangedToFalseEventProperty =
                serializedObject.FindProperty(_onValueChangedToFalseEventPropertyPath);
            EditorGUILayout.PropertyField(onValueChangedToFalseEventProperty, true);
        }

        protected override void PostOnInspectorGUI()
        {
            base.PostOnInspectorGUI();
            var compositeProperty = serializedObject.FindProperty(_compositePropertyPath);
            EditorGUILayout.PropertyField(compositeProperty, true);
            var compositeValue = compositeProperty.boolValue;
            if (compositeValue)
            {
                var memberValuesProperty = serializedObject.FindProperty(_memberValuesPropertyPath);
                var andOrProperty = serializedObject.FindProperty(_andOrPropertyPath);
                if (memberValuesProperty.arraySize > 1)
                    EditorGUILayout.PropertyField(andOrProperty, true);
                EditorGUILayout.PropertyField(memberValuesProperty, true);
            }
        }

        protected override void DrawRuntimeValuePropertyField(SerializedProperty runtimeValueProperty,
            string label = null)
        {
            var compositeProperty = serializedObject.FindProperty(_compositePropertyPath);
            var compositeValue = compositeProperty.boolValue;
            if (!compositeValue)
                base.DrawRuntimeValuePropertyField(runtimeValueProperty, label);
            else
                base.DrawRuntimeValuePropertyField(runtimeValueProperty, "Composite Value");
        }
    }
}