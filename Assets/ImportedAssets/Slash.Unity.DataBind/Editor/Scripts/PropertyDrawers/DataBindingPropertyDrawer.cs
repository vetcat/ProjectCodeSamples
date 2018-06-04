// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBindingPropertyDrawer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.PropertyDrawers
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    ///   Property Drawer for <see cref="DataBinding" />.
    /// </summary>
    [CustomPropertyDrawer(typeof(DataBinding))]
    public class DataBindingPropertyDrawer : PropertyDrawer
    {
        #region Constants

        private const float LineHeight = 16f;

        private const float LineSpacing = 2f;

        #endregion

        #region Fields

        private SerializedProperty constantProperty;

        private SerializedProperty pathProperty;

        private SerializedProperty providerProperty;

        private SerializedProperty referenceProperty;

        private SerializedProperty targetTypeProperty;

        #endregion

        #region Public Methods and Operators

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            this.targetTypeProperty = property.FindPropertyRelative("Type");
            this.providerProperty = property.FindPropertyRelative("Provider");
            this.constantProperty = property.FindPropertyRelative("Constant");
            this.referenceProperty = property.FindPropertyRelative("Reference");
            this.pathProperty = property.FindPropertyRelative("Path");

            var targetType = (DataBindingType)this.targetTypeProperty.enumValueIndex;
            float targetTypeHeight = 0;
            switch (targetType)
            {
                case DataBindingType.Context:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(this.pathProperty);
                    break;
                case DataBindingType.Provider:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(this.providerProperty);
                    break;
                case DataBindingType.Constant:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(this.constantProperty);
                    break;
                case DataBindingType.Reference:
                    targetTypeHeight = EditorGUI.GetPropertyHeight(this.referenceProperty);
                    break;
            }

            return LineHeight + targetTypeHeight + LineSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            var contentPosition = EditorGUI.PrefixLabel(position, label);
            if (this.targetTypeProperty != null)
            {
                // Type selection.
                contentPosition.height = LineHeight;
                EditorGUI.PropertyField(contentPosition, this.targetTypeProperty, GUIContent.none);
                position.y += LineHeight + LineSpacing;

                // Draw type specific fields.
                EditorGUI.indentLevel++;
                var targetType = (DataBindingType)this.targetTypeProperty.enumValueIndex;
                switch (targetType)
                {
                    case DataBindingType.Context:
                    {
                        var rect = new Rect(position) { height = LineHeight };
                        EditorGUI.PropertyField(rect, this.pathProperty);
                    }
                        break;
                    case DataBindingType.Provider:
                    {
                        var rect = new Rect(position) { height = LineHeight };
                        EditorGUI.PropertyField(rect, this.providerProperty, new GUIContent("Provider"));
                    }
                        break;
                    case DataBindingType.Constant:
                    {
                        var rect = new Rect(position) { height = LineHeight };
                        EditorGUI.PropertyField(rect, this.constantProperty, new GUIContent("Constant"));
                    }
                        break;
                    case DataBindingType.Reference:
                    {
                        var rect = new Rect(position) { height = LineHeight };
                        EditorGUI.ObjectField(rect, this.referenceProperty, new GUIContent("Reference"));
                    }
                        break;
                }
            }
            --EditorGUI.indentLevel;

            EditorGUI.EndProperty();
        }

        #endregion
    }
}