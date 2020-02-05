using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace SOA.Base
{
    public abstract class ReferenceDrawer<R, V, T, E, EE> : PropertyDrawer where R :
        Reference<V, T, E, EE>
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        private static readonly float _breakLine = 2f;
        private static readonly float _marginRight = 2f;

        //TODO Dynamically determine these string via member information of the class
        private static readonly string _persistencePropertyPath = "_persistence";
        private static readonly string _scopePropertyPath = "_scope";
        private static readonly string _localValuePropertyPath = "_localValue";
        private static readonly string _globalValuePropertyPath = "_globalValue";
        private static readonly string _prevGlobalValuePropertyPath = "_prevGlobalValue";

        private static readonly string _onValueChangedPropertyPath = "_onValueChangedEvent";
        private static readonly string _onValueChangedWithHistoryPropertyPath = "_onValueChangedWithHistoryEvent";
        private static readonly string _foldOutEventsPropertyPath = "_foldOutEvents";
        private static readonly string _onChangeEventsFoldOutHeader = "On Change Events";

        //TODO Move this to a utility inspector/UI class
        private static Color _wrongFieldColor = new Color(1, 0.75f, 0, .25f);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Begin
            label = EditorGUI.BeginProperty(position, label, property);

            //Check if property is part of array
            var isPartOfArray = property.IsPartOfArray();

            var originalIndentLevel = EditorGUI.indentLevel;

            EditorGUI.PrefixLabel(position, label);

            var contentRect = EditorGUI.PrefixLabel(position, label);

            if (isPartOfArray)
                EditorGUI.indentLevel -= 1;

            #region Usage (Scope & Persistence)

            //Get properties for usage section
            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var persistenceProperty = property.FindPropertyRelative(_persistencePropertyPath);
            var scopePropertyHeight = EditorGUI.GetPropertyHeight(scopeProperty);
            var persistencePropertyHeight = EditorGUI.GetPropertyHeight(persistenceProperty);

            //Get rects for usage sections
            var usageRectHeight = scopePropertyHeight > persistencePropertyHeight
                ? scopePropertyHeight
                : persistencePropertyHeight;
            var usageRect = new Rect
            (
                contentRect.position.x,
                contentRect.position.y,
                contentRect.width,
                usageRectHeight
            );
            var halfSplitUsageRectWidth = (usageRect.width - _marginRight) / 2;
            var firstHalfSplitUsageRect = new Rect
            (
                usageRect.position.x,
                usageRect.position.y,
                halfSplitUsageRectWidth,
                usageRect.height
            );
            var secondHalfSplitUsageRect = new Rect
            (
                usageRect.position.x + halfSplitUsageRectWidth + _marginRight,
                usageRect.y,
                halfSplitUsageRectWidth,
                usageRect.height
            );

            //Draw usage section
            EditorGUI.PropertyField(firstHalfSplitUsageRect, scopeProperty, GUIContent.none, true);
            EditorGUI.PropertyField(secondHalfSplitUsageRect, persistenceProperty, GUIContent.none, true);

            //Get values from usage properties
            var scope = (Scope) scopeProperty.enumValueIndex;
            var persistence = (Persistence) persistenceProperty.enumValueIndex;

            #endregion

            var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);
            var localValueProperty = property.FindPropertyRelative(_localValuePropertyPath);
            var valueRect = new Rect();
            //Draw value fields
            switch (scope)
            {
                case Scope.Local:
                    valueRect = DrawValuePropertyField(localValueProperty, usageRect, contentRect);
                    break;
                case Scope.Global:
                    valueRect = DrawValuePropertyField(globalValueProperty, usageRect, contentRect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (persistence)
            {
                case Persistence.Constant:
                    /*
                    if (globalValueProperty.objectReferenceValue != null && scope == Scope.Global)
                        if (((V) globalValueProperty.objectReferenceValue).Persistence == Persistence.Variable)
                        {
                            EditorGUI.DrawRect(valueRect, _wrongFieldColor);
                            Debug.LogWarning(
                                $"{globalValueProperty.objectReferenceValue.name} is referencing a variable. Set reference type to variable or change {globalValueProperty.objectReferenceValue.name} to be used as a constant.",
                                property.serializedObject.targetObject);
                        }
                    */
                    break;
                case Persistence.Variable:
                    DrawOnChangeEvents(contentRect, valueRect, isPartOfArray, property);
                    //Throw warning if reference is referencing a constant while referring to it as a variable
                    if (globalValueProperty.objectReferenceValue != null && scope == Scope.Global)
                        if (((V) globalValueProperty.objectReferenceValue).Persistence == Persistence.Constant)
                        {
                            EditorGUI.DrawRect(valueRect, _wrongFieldColor);
                            Debug.LogWarning(
                                $"{globalValueProperty.objectReferenceValue.name} is referencing a constant. Set reference type to constant or change {globalValueProperty.objectReferenceValue.name} to be used as a variable.",
                                property.serializedObject.targetObject);
                        }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AutoListen(property);

            EditorGUI.indentLevel = originalIndentLevel;
            EditorGUI.EndProperty();
        }

        private void AutoListen(SerializedProperty property)
        {
            var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);
            var prevGlobalValueProperty = property.FindPropertyRelative(_prevGlobalValuePropertyPath);
            Object globalValueValue = null;
            try
            {
                globalValueValue = globalValueProperty.objectReferenceValue == null
                    ? null
                    : globalValueProperty.objectReferenceValue;
            }
            catch (NullReferenceException)
            {
            }

            Object prevGlobalValueValue = null;
            try
            {
                prevGlobalValueValue = prevGlobalValueProperty.objectReferenceValue == null
                    ? null
                    : prevGlobalValueProperty.objectReferenceValue;
            }
            catch (NullReferenceException)
            {
            }

            if (property.GetValue() is Reference<V, T, E, EE> reference)
            {
                if (reference.PrevGlobalValue != (V) globalValueValue)
                {
                    if (reference.PrevGlobalValue != null)
                    {
                        var prevGlobalValue = reference.PrevGlobalValue;
                        if (prevGlobalValue != null)
                            reference.RemoveAutoListeners(prevGlobalValue);
                    }

                    if (globalValueValue != null)
                    {
                        var globalValue = globalValueValue as V;
                        reference.AddAutoListeners(globalValue);
                        
                    }
                    if(globalValueValue != null)
                        reference.PrevGlobalValue = globalValueValue as V;
                    else
                        reference.PrevGlobalValue = null;

                }
            }
        }

        private static Rect DrawValuePropertyField(SerializedProperty valueProperty, Rect usageRect, Rect contentRect)
        {
            var valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty);
            var valueRect = new Rect
            (
                usageRect.position.x,
                usageRect.position.y + usageRect.height + _breakLine,
                contentRect.width,
                valuePropertyHeight
            );
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none, true);
            return valueRect;
        }

        private void DrawOnChangeEvents(Rect contentRect, Rect valueRect, bool isPartOfArray,
            SerializedProperty property)
        {
            {
                var eventsFoldOutRect = new Rect(
                    contentRect.position.x,
                    valueRect.position.y + valueRect.height,
                    contentRect.width,
                    EditorGUIUtility.singleLineHeight);
                eventsFoldOutRect = EditorGUI.IndentedRect(eventsFoldOutRect);
                EditorGUI.indentLevel += 1;
                property.FindPropertyRelative(_foldOutEventsPropertyPath).boolValue = EditorGUI.Foldout(
                    eventsFoldOutRect, property.FindPropertyRelative(_foldOutEventsPropertyPath).boolValue,
                    _onChangeEventsFoldOutHeader);

                if (property.FindPropertyRelative(_foldOutEventsPropertyPath).boolValue)
                {
                    if (!isPartOfArray)
                        EditorGUI.indentLevel += 1;
                    var onValueChangedProp = property.FindPropertyRelative(_onValueChangedPropertyPath);
                    var onValueChangedRect = new Rect(
                        contentRect.position.x,
                        eventsFoldOutRect.position.y + eventsFoldOutRect.height,
                        contentRect.width,
                        EditorGUI.GetPropertyHeight(onValueChangedProp) + _breakLine);
                    onValueChangedRect = EditorGUI.IndentedRect(onValueChangedRect);
                    //EditorGUI.DrawRect(onValueChangedRect, Color.cyan);
                    EditorGUI.PropertyField(onValueChangedRect, onValueChangedProp,
                        new GUIContent(onValueChangedProp.displayName), true);

                    var onValueChangedWithHistoryProp =
                        property.FindPropertyRelative(_onValueChangedWithHistoryPropertyPath);
                    var onValueChangedWithHistoryRect = new Rect(
                        contentRect.position.x,
                        onValueChangedRect.position.y + onValueChangedRect.height,
                        contentRect.width,
                        EditorGUI.GetPropertyHeight(onValueChangedWithHistoryProp) + _breakLine);
                    onValueChangedWithHistoryRect = EditorGUI.IndentedRect(onValueChangedWithHistoryRect);
                    //EditorGUI.DrawRect(onValueChangedWithHistoryRect, Color.green);
                    EditorGUI.PropertyField(onValueChangedWithHistoryRect, onValueChangedWithHistoryProp,
                        new GUIContent(onValueChangedWithHistoryProp.displayName), true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var persistenceProperty = property.FindPropertyRelative(_persistencePropertyPath);
            var scopePropertyHeight = EditorGUI.GetPropertyHeight(scopeProperty);
            var persistencePropertyHeight = EditorGUI.GetPropertyHeight(persistenceProperty);
            var scope = (Scope) scopeProperty.enumValueIndex;
            var persistence = (Persistence) persistenceProperty.enumValueIndex;
            var usageHeight = scopePropertyHeight > persistencePropertyHeight
                ? scopePropertyHeight
                : persistencePropertyHeight;
            float valueHeight;
            switch (scope)
            {
                case Scope.Local:
                    valueHeight = property.FindPropertyRelativeAndGetHeight(_localValuePropertyPath);
                    break;
                case Scope.Global:
                    valueHeight = property.FindPropertyRelativeAndGetHeight(_globalValuePropertyPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var usageAndValueHeight = usageHeight + _breakLine + valueHeight;
            switch (persistence)
            {
                case Persistence.Constant:

                    return usageAndValueHeight;
                case Persistence.Variable:
                {
                    var eventsFoldoutHeight = EditorGUIUtility.singleLineHeight;
                    var onValueChangedHeight = property.FindPropertyRelativeAndGetHeight(_onValueChangedPropertyPath);
                    var onValueChangedWithHistoryHeight =
                        property.FindPropertyRelativeAndGetHeight(_onValueChangedWithHistoryPropertyPath);
                    var foldedInHeight = usageAndValueHeight + eventsFoldoutHeight;
                    var foldedOutHeight = foldedInHeight + _breakLine + onValueChangedHeight + _breakLine +
                                          onValueChangedWithHistoryHeight + EditorGUIUtility.singleLineHeight;
                    return property.FindPropertyRelative(_foldOutEventsPropertyPath).boolValue
                        ? foldedOutHeight
                        : foldedInHeight;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}