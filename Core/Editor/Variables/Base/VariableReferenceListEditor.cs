using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public class VariableReferenceListEditor<VRL, RL, R, V, T, E, EE, RE> : Editor
        where VRL : ReferenceListVariable<RL, R, V, T, E, EE, RE>
        where RL : ReferenceList<R, V, T, E, EE, RE>, new()
        where R : Reference<V, T, E, EE>, new()
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
        where RE : UnityEvent<R>, new()

    {
        //TODO Get these fields dynamically
        private static readonly string _referenceListPropertyPath = "_items";
        private static readonly string _referencesPropertyPath = "_items";
        private static readonly string _eventsFoldedOutPropertyPath = "_eventsFoldedOut";
        private static readonly string _onAddedPropertyPath = "_onAddedEvent";
        private static readonly string _onRemovedPropertyPath = "_onRemovedEvent";

        private static readonly string _referenceListHeader = "Items";
        private static readonly string _eventsFoldedOutHeader = "On Items Changed Events";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var referenceListProperty = serializedObject.FindProperty(_referenceListPropertyPath);
            var references = referenceListProperty.FindPropertyRelative(_referencesPropertyPath);
            var eventsFoldedOutProperty = referenceListProperty.FindPropertyRelative(_eventsFoldedOutPropertyPath);

            EditorGUILayout.PropertyField(references, new GUIContent(_referenceListHeader), true);

            eventsFoldedOutProperty.boolValue =
                EditorGUILayout.Foldout(eventsFoldedOutProperty.boolValue, _eventsFoldedOutHeader);
            if (eventsFoldedOutProperty.boolValue)
            {
                var onAddedProperty = referenceListProperty.FindPropertyRelative(_onAddedPropertyPath);
                EditorGUILayout.PropertyField(onAddedProperty, true);
                var onRemovedProperty = referenceListProperty.FindPropertyRelative(_onRemovedPropertyPath);
                EditorGUILayout.PropertyField(onRemovedProperty, true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}