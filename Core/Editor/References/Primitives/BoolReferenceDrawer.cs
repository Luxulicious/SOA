using System;
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
        protected static readonly string _invertPropertyPath = "_invertResult";
        protected string _onValueChangedToFalsePropertyPath = "_onValueChangedToTrueEvent";
        protected string _onValueChangedToTruePropertyPath = "_onValueChangedToFalseEvent";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }

        protected override Rect DrawValueFields(SerializedProperty property, Rect usageRect)
        {
            var newRect = base.DrawValueFields(property, usageRect);
            var invertProperty = property.FindPropertyRelative(_invertPropertyPath);
            var invertPropertyHeight = EditorGUI.GetPropertyHeight(invertProperty);
            var invertRect =
                new Rect(
                    newRect.position.x,
                    newRect.position.y + newRect.height + _breakLine,
                    newRect.width,
                    invertPropertyHeight
                );
            EditorGUI.PropertyField(
                invertRect, invertProperty, true);
            newRect = new Rect(newRect.position.x, newRect.position.y, newRect.width,
                newRect.height + invertPropertyHeight + _breakLine);
            return newRect;
        }
        
        //TODO Use this in override of DrawValueProperty
        private Rect DrawValueField(Func<SerializedProperty, Rect, Rect> drawField, SerializedProperty property, Rect valueRect)
        {
            var newRect = drawField(property, valueRect);

            var invertProperty = property.FindPropertyRelative(_invertPropertyPath);
            if (invertProperty.boolValue)
            {
                var invertedPrefix = "!";
                var invertedPrefixLabelSize = GUI.skin.label.CalcSize(new GUIContent(invertedPrefix));
                var invertedPrefixRect =
                    new Rect(
                        newRect.position.x,
                        newRect.position.y,
                        invertedPrefixLabelSize.x,
                        newRect.height
                    );
                EditorGUI.LabelField(invertedPrefixRect, invertedPrefix);
                newRect =
                    new Rect(
                        newRect.position.x + invertedPrefixLabelSize.x,
                        newRect.position.y,
                        newRect.width + invertedPrefixLabelSize.x,
                        newRect.height
                    );
            }

            return newRect;
        }

        protected override Rect DrawOnValueChangedEvents(SerializedProperty property, Rect eventsFoldOutRect)
        {
            var baseRect = base.DrawOnValueChangedEvents(property, eventsFoldOutRect);

            var onValueChangedToTrueProperty = property.FindPropertyRelative(_onValueChangedToTruePropertyPath);
            var onValueChangedToTrueRect = new Rect(
                eventsFoldOutRect.position.x,
                baseRect.position.y + baseRect.height,
                eventsFoldOutRect.width,
                EditorGUI.GetPropertyHeight(onValueChangedToTrueProperty) + _breakLine);
            onValueChangedToTrueRect = EditorGUI.IndentedRect(onValueChangedToTrueRect);
            EditorGUI.PropertyField(onValueChangedToTrueRect, onValueChangedToTrueProperty,
                new GUIContent(onValueChangedToTrueProperty.displayName), true);

            var onValueChangedToFalseProperty =
                property.FindPropertyRelative(_onValueChangedToFalsePropertyPath);
            var onValueChangedToFalseRect = new Rect(
                eventsFoldOutRect.position.x,
                onValueChangedToTrueRect.position.y + onValueChangedToTrueRect.height,
                eventsFoldOutRect.width,
                EditorGUI.GetPropertyHeight(onValueChangedToFalseProperty) + _breakLine);
            onValueChangedToFalseRect = EditorGUI.IndentedRect(onValueChangedToFalseRect);
            EditorGUI.PropertyField(onValueChangedToFalseRect, onValueChangedToFalseProperty,
                new GUIContent(onValueChangedToFalseProperty.displayName), true);
            var latestRect = onValueChangedToFalseRect;
            return latestRect;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var invertProperty = property.FindPropertyRelative(_invertPropertyPath);

            var newHeight = base.GetPropertyHeight(property, label);
            newHeight += EditorGUI.GetPropertyHeight(invertProperty);

            if (property.FindPropertyRelative(_foldoutEventsPropertyPath).boolValue)
            {
                var onValueChangedToTruePropertyHeight =
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative(_onValueChangedToTruePropertyPath));
                newHeight += _breakLine + onValueChangedToTruePropertyHeight;
                var onValueChangedToFalsePropertyHeight =
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative(_onValueChangedToFalsePropertyPath));
                newHeight += _breakLine + onValueChangedToFalsePropertyHeight;
            }

            return newHeight;
        }
    }
}