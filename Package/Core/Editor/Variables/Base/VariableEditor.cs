using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class VariableEditor<V, T, E, EE> : Editor where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        //TODO Dynamically determine these string via member information of the class
        protected static readonly string _persistencePropertyPath = "_persistence";
        protected static readonly string _defaultValuePropertyPath = "_defaultValue";
        protected static readonly string _runtimeValuePropertyPath = "_runtimeValue";
        protected static readonly string _foldOutOnChangeEventsPropertyPath = "_foldOutOnChangeEvents";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            PreOnInspectorGUI();

            var defaultValueProperty = serializedObject.FindProperty(_defaultValuePropertyPath);
            var runtimeValueProperty = serializedObject.FindProperty(_runtimeValuePropertyPath);
            var useAsConstantProperty = serializedObject.FindProperty(_persistencePropertyPath);

            var isPlaying = Application.isPlaying;

            if (!isPlaying)
                DrawPersistencePropertyField(useAsConstantProperty);

            if (useAsConstantProperty.boolValue)
            {
                var constantValueLabel = "Constant Value";
                if (!isPlaying)
                    DrawDefaultValuePropertyField(defaultValueProperty, constantValueLabel);
                else
                    DrawRuntimeValuePropertyField(runtimeValueProperty, constantValueLabel);
            }
            else
            {
                if (runtimeValueProperty.IsPossibleRunTime())
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(defaultValueProperty, true);
                    EditorGUI.EndDisabledGroup();

                    if (isPlaying)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        var runtimeValue = runtimeValueProperty.GetValue();
                        var valueString = runtimeValue?.ToString();
                        var displayValue = runtimeValue != null
                            ? $"{valueString}"
                            : $"None ({typeof(T)})";
                        EditorGUILayout.TextField(runtimeValueProperty.displayName,
                            displayValue);
                        EditorGUI.EndDisabledGroup();

                        if (GUILayout.Button("Go to reference"))
                        {
                            var instance = runtimeValue as GameObject;
                            if (instance == null) instance = (runtimeValue as Component)?.gameObject;
                            var instanceId = instance.GetInstanceID();
                            Selection.activeInstanceID = instanceId;
                            Selection.activeGameObject = instance;
                        }

                        if (GUILayout.Button("Clear reference")) runtimeValueProperty.SetValue(null);
                    }
                    else
                    {
                        if (runtimeValueProperty.GetValue() != null)
                            runtimeValueProperty.SetValue(null);
                        EditorGUI.BeginDisabledGroup(true);
                        DrawRuntimeValuePropertyField(runtimeValueProperty);
                        EditorGUI.EndDisabledGroup();
                    }

                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField(
                        $"({typeof(T).Name} Variables can only be assigned to at runtime without inspector)");
                    EditorGUI.EndDisabledGroup();
                }
                else
                {
                    EditorGUI.BeginDisabledGroup(isPlaying);
                    DrawDefaultValuePropertyField(defaultValueProperty);
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(!isPlaying);
                    DrawRuntimeValuePropertyField(runtimeValueProperty);
                    EditorGUI.EndDisabledGroup();
                }

                var foldOutOnChangedEvents = serializedObject.FindProperty(_foldOutOnChangeEventsPropertyPath);
                foldOutOnChangedEvents.boolValue = EditorGUILayout.Foldout(foldOutOnChangedEvents.boolValue, "On Change Events");
                if (foldOutOnChangedEvents.boolValue)
                {
                    var onChangedEventProperty = serializedObject.FindProperty("_onChangeEvent");
                    var onChangedWithHistoryEventProperty = serializedObject.FindProperty("_onChangeWithHistoryEvent");
                    DrawOnChangeEventsPropertyFields(onChangedEventProperty, onChangedWithHistoryEventProperty);

                    EditorGUI.BeginDisabledGroup(!isPlaying);
                    var v = target as V;
                    DrawInvokeOnChangeEventsButtons(v);
                    EditorGUI.EndDisabledGroup();
                }
            }

            PostOnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawInvokeOnChangeEventsButtons(V variable)
        {
            if (GUILayout.Button("Invoke On Change Event")) variable.ForceInvokeOnChangeEvent();
            if (GUILayout.Button("Invoke On Change With History Event")) variable.ForceInvokeOnChangeWithHistoryEvent();
        }

        protected virtual void DrawOnChangeEventsPropertyFields(SerializedProperty onChangedEventProperty,
            SerializedProperty onChangedWithHistoryEventProperty)
        {
            DrawOnChangedEventProperty(onChangedEventProperty);
            DrawOnChangedWithHistoryEventProperty(onChangedWithHistoryEventProperty);
        }

        protected virtual void DrawOnChangedWithHistoryEventProperty(
            SerializedProperty onChangedWithHistoryEventProperty)
        {
            EditorGUILayout.PropertyField(onChangedWithHistoryEventProperty, true);
        }

        protected virtual void DrawOnChangedEventProperty(SerializedProperty onChangedEventProperty)
        {
            EditorGUILayout.PropertyField(onChangedEventProperty, true);
        }

        protected virtual void DrawDefaultValuePropertyField(SerializedProperty defaultValueProperty,
            string label = null)
        {
            if (label != null)
                EditorGUILayout.PropertyField(defaultValueProperty, new GUIContent(label), true);
            else
                EditorGUILayout.PropertyField(defaultValueProperty, true);
        }

        protected virtual void DrawRuntimeValuePropertyField(SerializedProperty runtimeValueProperty,
            string label = null)
        {
            if (label != null)
                EditorGUILayout.PropertyField(runtimeValueProperty, new GUIContent(label), true);
            else
                EditorGUILayout.PropertyField(runtimeValueProperty, true);
        }

        //TODO Abstract into interface
        /// <summary>
        /// Override this to run code pre-OnInspectorGUI()
        /// </summary>
        protected virtual void PreOnInspectorGUI()
        {
            return;
        }

        //TODO Abstract into interface
        /// <summary>
        /// Override this to run code post-OnInspectorGUI()
        /// </summary>
        protected virtual void PostOnInspectorGUI()
        {
            return;
        }

        protected virtual void DrawPersistencePropertyField(SerializedProperty useAsConstantProperty)
        {
            EditorGUILayout.PropertyField(useAsConstantProperty, true);
        }
    }
}