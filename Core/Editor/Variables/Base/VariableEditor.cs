using System.Collections.Generic;
using System.Linq;
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
        protected static readonly string _foldOutUsesPropertyPath = "_foldOutUses";

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

                var foldOutOnValueChangedEvents = serializedObject.FindProperty(_foldOutOnChangeEventsPropertyPath);
                foldOutOnValueChangedEvents.boolValue =
                    EditorGUILayout.Foldout(foldOutOnValueChangedEvents.boolValue, "On Change Events");
                if (foldOutOnValueChangedEvents.boolValue)
                {
                    var onChangedEventProperty = serializedObject.FindProperty("_onValueChangedEvent");
                    var onChangedWithHistoryEventProperty =
                        serializedObject.FindProperty("_onValueChangedWithHistoryEvent");
                    DrawOnChangeEventsPropertyFields(onChangedEventProperty, onChangedWithHistoryEventProperty);

                    EditorGUI.BeginDisabledGroup(!isPlaying);
                    var v = target as V;
                    DrawInvokeOnChangeEventsButtons(v);
                    EditorGUI.EndDisabledGroup();
                }
            }

            PostOnChangedEvents();

            //TODO Draw references form
            DrawUses();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawUses()
        {
            //TODO Replace with serialized property
            var foldOutUsesProperty = serializedObject.FindProperty(_foldOutUsesPropertyPath);
            foldOutUsesProperty.boolValue = EditorGUILayout.Foldout(foldOutUsesProperty.boolValue, "Uses");
            if (foldOutUsesProperty.boolValue)
            {
                EditorGUI.indentLevel += 1;

                /*
                EditorGUI.BeginDisabledGroup(true);
                if (GUILayout.Button("Refresh"))
                {
                    var registeredReferenceContainers =
                        FindObjectsOfType<MonoBehaviour>().OfType<IRegisteredReferenceContainer>();
                    foreach (var registeredReferenceContainer in registeredReferenceContainers)
                        registeredReferenceContainer.Register();
                }
                EditorGUI.EndDisabledGroup();
                */

                //Get target variable
                var variable = serializedObject.targetObject as Variable<T, E, EE>;
                //Get registrations
                //TODO Add References from events as well...
                var registrations = variable?.Uses;
                //Get registered referenced containers as UnityEngine.Object
                var registeredReferenceContainers = registrations?.Keys.Select(x => x as Object);
                //Filter out any null references just in case
                registeredReferenceContainers = registeredReferenceContainers?.Where(x => x != null);
                //Removed prefabs TODO add these back in a different category
                //TODO Also maybe separate scriptable objects as well
                var newRegisteredReferenceContainers = registeredReferenceContainers?.Where(x =>
                    x is GameObject gameObject && gameObject.scene.isLoaded ||
                    x is ScriptableObject ||
                    x is Component component && component.gameObject.scene.isLoaded
                );
                //Order alphabetically
                newRegisteredReferenceContainers = newRegisteredReferenceContainers?.OrderBy(x => x?.name);
                //Draw the uses
                if (newRegisteredReferenceContainers != null)
                    foreach (var referencedObject in newRegisteredReferenceContainers)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("", referencedObject,
                            typeof(Object), true);
                        EditorGUI.EndDisabledGroup();
                        if (GUILayout.Button("Ping")) EditorGUIUtility.PingObject(referencedObject);

                        if (GUILayout.Button("Select")) Selection.activeObject = referencedObject;
                        EditorGUILayout.EndHorizontal();
                    }

                EditorGUI.indentLevel -= 1;
            }

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
        protected virtual void PostOnChangedEvents()
        {
            return;
        }

        protected virtual void DrawPersistencePropertyField(SerializedProperty useAsConstantProperty)
        {
            EditorGUILayout.PropertyField(useAsConstantProperty, true);
        }
    }
}