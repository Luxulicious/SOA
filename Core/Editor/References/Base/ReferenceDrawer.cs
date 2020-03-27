using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SOA.Base
{
    public abstract class ReferenceDrawer<R, V, T, E, EE> : PropertyDrawer where R :
        Reference<V, T, E, EE>
        where V : Variable<T, E, EE>
        where E : UnityEvent<T>, new()
        where EE : UnityEvent<T, T>, new()
    {
        protected static readonly float _breakLine = 2f;
        protected static readonly float _marginRight = 2f;

        //TODO Dynamically determine these string via member information of the class

        #region Property Paths

        protected static readonly string _persistencePropertyPath = "_persistence";
        protected static readonly string _scopePropertyPath = "_scope";
        protected static readonly string _localValuePropertyPath = "_localValue";
        protected static readonly string _globalValuePropertyPath = "_globalValue";
        protected static readonly string _prevGlobalValuePropertyPath = "_prevGlobalValue";
        protected static readonly string _mainFoldOutPropertyPath = "_foldout";
        protected static readonly string _onValueChangedPropertyPath = "_onValueChangedEvent";
        protected static readonly string _onValueChangedWithHistoryPropertyPath = "_onValueChangedWithHistoryEvent";
        protected static readonly string _foldoutEventsPropertyPath = "_foldoutEvents";

        #endregion

        private static readonly string _onChangeEventsFoldoutHeader = "On Change Events";

        //TODO Move this to a utility inspector/UI class
        private static Color _wrongFieldColor = new Color(1, 0.75f, 0, .25f);

        /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Begin Label
            label = EditorGUI.BeginProperty(position, label, property);

            //Check if property is part of array
            var isPartOfArray = property.IsPartOfArray();

            var originalIndentLevel = EditorGUI.indentLevel;

            property.FindPropertyRelative(_foldoutPropertyPath).boolValue = EditorGUI.Foldout(
                new Rect(position.x, position.y, 5, position.height), property.FindPropertyRelative(_foldoutPropertyPath).boolValue,
                !property.FindPropertyRelative(_foldoutPropertyPath).boolValue ? property.displayName : "");

            var isFoldedOut = property.FindPropertyRelative(_foldoutPropertyPath).boolValue;

            if (isFoldedOut)
            {
                var labelRect = EditorGUI.PrefixLabel(position, label);

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
                    contentRect.position.y + _breakLine,
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
                var scope = (Scope)scopeProperty.enumValueIndex;
                var persistence = (Persistence)persistenceProperty.enumValueIndex;

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
                        break;
                    case Persistence.Variable:
                        DrawOnChangeEvents(contentRect, valueRect, isPartOfArray, property);
                        //Throw warning if reference is referencing a constant while referring to it as a variable
                        if (globalValueProperty.objectReferenceValue != null && scope == Scope.Global)
                            if (((V)globalValueProperty.objectReferenceValue).Persistence == Persistence.Constant)
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
            }
            else
            {
                var labelRect = EditorGUI.PrefixLabel(position, label);
                var contentRect = EditorGUI.PrefixLabel(position, label);

                var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
                var scope = (Scope)scopeProperty.enumValueIndex;
                switch (scope)
                {
                    case Scope.Local:
                        var localValueProperty = property.FindPropertyRelative(_localValuePropertyPath);
                        EditorGUI.PropertyField(new Rect(contentRect.x, contentRect.y, contentRect.width, EditorGUI.GetPropertyHeight(localValueProperty)), localValueProperty, GUIContent.none);
                        break;
                    case Scope.Global:
                        var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);
                        EditorGUI.PropertyField(new Rect(contentRect.x, contentRect.y, contentRect.width, EditorGUI.GetPropertyHeight(globalValueProperty)), globalValueProperty, GUIContent.none);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            PostOnInspectorGUI(position, property, label);

            EditorGUI.indentLevel = originalIndentLevel;
            EditorGUI.EndProperty();
        }*/

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Begin Label
            label = EditorGUI.BeginProperty(position, label, property);

            //Check if property is part of array
            var isPartOfArray = property.IsPartOfArray();

            var originalIndentLevel = EditorGUI.indentLevel;

            var mainFoldOutRect = new Rect(position.x, position.y, 5, position.height);
            property.FindPropertyRelative(_mainFoldOutPropertyPath).boolValue =
                EditorGUI.Foldout(
                    mainFoldOutRect,
                    property.FindPropertyRelative(_mainFoldOutPropertyPath).boolValue,
                    !property.FindPropertyRelative(_mainFoldOutPropertyPath).boolValue ? property.displayName : ""
                );

            var mainFoldOutValue = property.FindPropertyRelative(_mainFoldOutPropertyPath).boolValue;

            if (mainFoldOutValue)
            {
                DrawFoldedOutReference(position, property, label);
            }
            else
            {
                DrawUnFoldedOutReference(position, property, label);
            }

            PostOnInspectorGUI(position, property, label);

            EditorGUI.indentLevel = originalIndentLevel;
            EditorGUI.EndProperty();
        }

        protected virtual void DrawUnFoldedOutReference(Rect position, SerializedProperty property, GUIContent label)
        {
            var labelRect = EditorGUI.PrefixLabel(position, label);
            var contentRect = EditorGUI.PrefixLabel(position, label);

            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var scope = (Scope) scopeProperty.enumValueIndex;
            switch (scope)
            {
                case Scope.Local:
                    var localValueProperty = property.FindPropertyRelative(_localValuePropertyPath);
                    EditorGUI.PropertyField(
                        new Rect(contentRect.x, contentRect.y, contentRect.width,
                            EditorGUI.GetPropertyHeight(localValueProperty)), localValueProperty, GUIContent.none);
                    break;
                case Scope.Global:
                    var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);
                    EditorGUI.PropertyField(
                        new Rect(contentRect.x, contentRect.y, contentRect.width,
                            EditorGUI.GetPropertyHeight(globalValueProperty)), globalValueProperty,
                        GUIContent.none);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void DrawFoldedOutReference(Rect position, SerializedProperty property, GUIContent label)
        {
            var isPartOfArray = property.IsPartOfArray();

            var contentRect = EditorGUI.PrefixLabel(position, label);

            if (isPartOfArray)
                EditorGUI.indentLevel -= 1;

            var usageRect = DrawUsageField(property, contentRect);

            var valueRect = DrawValueFields(property, usageRect);

            //Get properties usage properties
            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var persistenceProperty = property.FindPropertyRelative(_persistencePropertyPath);
            //Get values from usage properties
            var scope = (Scope) scopeProperty.enumValueIndex;
            var persistence = (Persistence) persistenceProperty.enumValueIndex;
            var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);

            #region Checking for faulty Persistence 

            switch (persistence)
            {
                case Persistence.Constant:
                    break;
                case Persistence.Variable:
                    DrawOnValueChangedEventsFoldout(valueRect, property);
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

            #endregion
        }

        protected virtual Rect DrawValueFields(SerializedProperty property, Rect usageRect)
        {
            //Get properties usage properties
            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var persistenceProperty = property.FindPropertyRelative(_persistencePropertyPath);
            //Get values from usage properties
            var scope = (Scope) scopeProperty.enumValueIndex;
            var persistence = (Persistence) persistenceProperty.enumValueIndex;
            var valueRect = new Rect();
            //Draw value fields
            switch (scope)
            {
                case Scope.Local:
                    valueRect = DrawLocalValueField(property, usageRect);
                    break;
                case Scope.Global:
                    valueRect = DrawGlobalValueField(property, usageRect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return valueRect;
        }

        protected virtual Rect DrawGlobalValueField(SerializedProperty property, Rect usageRect)
        {
            var globalValueProperty = property.FindPropertyRelative(_globalValuePropertyPath);
            var valueRect = DrawValuePropertyField(globalValueProperty, usageRect);
            return valueRect;
        }

        protected virtual Rect DrawLocalValueField(SerializedProperty property, Rect usageRect)
        {
            var localValueProperty = property.FindPropertyRelative(_localValuePropertyPath);
            var valueRect = DrawValuePropertyField(localValueProperty, usageRect);
            return valueRect;
        }

        protected virtual Rect DrawUsageField(SerializedProperty property, Rect contentRect)
        {
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
                contentRect.position.y + _breakLine,
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

            return usageRect;
        }

        protected virtual Rect DrawValuePropertyField(SerializedProperty valueProperty, Rect usageRect)
        {
            var valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty);
            var valueRect = new Rect
            (
                usageRect.position.x,
                usageRect.position.y + usageRect.height + _breakLine,
                usageRect.width,
                valuePropertyHeight
            );
            EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none, true);
            return valueRect;
        }

        protected virtual Rect DrawOnValueChangedEventsFoldout(Rect valueRect,
            SerializedProperty property)
        {
            var eventsFoldOutRect = new Rect(
                valueRect.position.x,
                valueRect.position.y + valueRect.height,
                valueRect.width,
                EditorGUIUtility.singleLineHeight);
            //eventsFoldOutRect = EditorGUI.IndentedRect(eventsFoldOutRect);
            EditorGUI.indentLevel += 1;
            property.FindPropertyRelative(_foldoutEventsPropertyPath).boolValue = EditorGUI.Foldout(
                eventsFoldOutRect, property.FindPropertyRelative(_foldoutEventsPropertyPath).boolValue,
                _onChangeEventsFoldoutHeader);

            if (property.FindPropertyRelative(_foldoutEventsPropertyPath).boolValue)
            {
                DrawOnValueChangedEvents(property, eventsFoldOutRect);
            }

            return eventsFoldOutRect;
        }

        protected virtual Rect DrawOnValueChangedEvents(SerializedProperty property, Rect eventsFoldOutRect)
        {
            var isPartOfArray = property.IsPartOfArray();
            if (!isPartOfArray)
                EditorGUI.indentLevel += 1;

            var onValueChangedProperty = property.FindPropertyRelative(_onValueChangedPropertyPath);
            var onValueChangedRect = new Rect(
                eventsFoldOutRect.position.x,
                eventsFoldOutRect.position.y + eventsFoldOutRect.height,
                eventsFoldOutRect.width,
                EditorGUI.GetPropertyHeight(onValueChangedProperty) + _breakLine);
            onValueChangedRect = EditorGUI.IndentedRect(onValueChangedRect);
            EditorGUI.PropertyField(onValueChangedRect, onValueChangedProperty,
                new GUIContent(onValueChangedProperty.displayName), true);

            var onValueChangedWithHistoryProperty =
                property.FindPropertyRelative(_onValueChangedWithHistoryPropertyPath);
            var onValueChangedWithHistoryRect = new Rect(
                eventsFoldOutRect.position.x,
                onValueChangedRect.position.y + onValueChangedRect.height,
                eventsFoldOutRect.width,
                EditorGUI.GetPropertyHeight(onValueChangedWithHistoryProperty) + _breakLine);
            onValueChangedWithHistoryRect = EditorGUI.IndentedRect(onValueChangedWithHistoryRect);
            EditorGUI.PropertyField(onValueChangedWithHistoryRect, onValueChangedWithHistoryProperty,
                new GUIContent(onValueChangedWithHistoryProperty.displayName), true);
            var latestRect = onValueChangedWithHistoryRect;
            return latestRect;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        {
            var scopeProperty = property.FindPropertyRelative(_scopePropertyPath);
            var persistenceProperty = property.FindPropertyRelative(_persistencePropertyPath);
            var scopePropertyHeight = EditorGUI.GetPropertyHeight(scopeProperty);
            var persistencePropertyHeight = EditorGUI.GetPropertyHeight(persistenceProperty);
            var scope = (Scope) scopeProperty.enumValueIndex;
            var persistence = (Persistence) persistenceProperty.enumValueIndex;
            var usageHeight = (scopePropertyHeight > persistencePropertyHeight
                                  ? scopePropertyHeight
                                  : persistencePropertyHeight) + +_breakLine;
            float valueHeight;
            switch (scope)
            {
                case Scope.Local:
                    valueHeight = property.GetRelativePropertyHeight(_localValuePropertyPath);
                    break;
                case Scope.Global:
                    valueHeight = property.GetRelativePropertyHeight(_globalValuePropertyPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var usageAndValueHeight = usageHeight + _breakLine + valueHeight;

            var isFoldedOut = property.FindPropertyRelative(_mainFoldOutPropertyPath).boolValue;
            if (isFoldedOut)
            {
                switch (persistence)
                {
                    case Persistence.Constant:
                        return usageAndValueHeight;
                    case Persistence.Variable:
                    {
                        var eventsFoldoutHeight = EditorGUIUtility.singleLineHeight;
                        var onValueChangedHeight = property.GetRelativePropertyHeight(_onValueChangedPropertyPath);
                        var onValueChangedWithHistoryHeight =
                            property.GetRelativePropertyHeight(_onValueChangedWithHistoryPropertyPath);
                        var foldedInHeight = usageAndValueHeight + eventsFoldoutHeight;
                        var foldedOutHeight = foldedInHeight + _breakLine + onValueChangedHeight + _breakLine +
                                              onValueChangedWithHistoryHeight + EditorGUIUtility.singleLineHeight;
                        return property.FindPropertyRelative(_foldoutEventsPropertyPath).boolValue
                            ? foldedOutHeight
                            : foldedInHeight;
                    }

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                return valueHeight;
            }
        }

        //TODO Abstract into interface
        /// <summary>
        /// Override this to run code pre-OnInspectorGUI()
        /// </summary>
        protected virtual void PostOnInspectorGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            return;
        }
    }
}