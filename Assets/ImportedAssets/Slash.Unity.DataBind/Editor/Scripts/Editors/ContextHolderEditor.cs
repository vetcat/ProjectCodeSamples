// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextHolderEditor.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;
    using Slash.Unity.DataBind.Editor.Utils;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    ///   Custom editor for <see cref="ContextHolder" />.
    /// </summary>
    [CustomEditor(typeof(ContextHolder))]
    public class ContextHolderEditor : Editor
    {
        #region Fields

        private readonly string[] contextTypeNames;

        private readonly List<Type> contextTypes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        public ContextHolderEditor()
        {
            this.contextTypes = new List<Type> { null };
            var availableContextTypes =
                ReflectionUtils.FindTypesWithBase<Context>().Where(type => !type.IsAbstract).ToList();
            availableContextTypes.Sort(
                (typeA, typeB) => String.Compare(typeA.FullName, typeB.FullName, StringComparison.Ordinal));
            this.contextTypes.AddRange(availableContextTypes);
            this.contextTypeNames = this.contextTypes.Select(type => type != null ? type.FullName : "None").ToArray();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Unity callback.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var contextHolder = this.target as ContextHolder;
            if (contextHolder == null)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            if (!Application.isPlaying)
            {
                // Find all available context classes.
                var contextTypeIndex = this.contextTypes.IndexOf(contextHolder.ContextType);
                var newContextTypeIndex = EditorGUILayout.Popup("Context Type", contextTypeIndex, this.contextTypeNames);
                if (newContextTypeIndex != contextTypeIndex)
                {
                    contextHolder.ContextType = this.contextTypes[newContextTypeIndex];
                }

                // Should a context of the specified type be created at startup?
                contextHolder.CreateContext =
                    EditorGUILayout.Toggle(
                        new GUIContent("Create context?", "Create context on startup?"),
                        contextHolder.CreateContext);
            }
            else
            {
                var context = contextHolder.Context;
                var contextType = context != null ? context.GetType().ToString() : "null";
                EditorGUILayout.LabelField("Context", contextType);

                // Reflect data in context.
                this.DrawContextData(context);

                EditorUtility.SetDirty(contextHolder);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(contextHolder);
            }
        }

        #endregion

        #region Methods

        private const int MaxLevel = 5;

        private void DrawContextData(object contextObject)
        {
            if (contextObject == null)
            {
                return;
            }

            var context = contextObject as Context;
            if (context != null)
            {
                this.DrawContextData(context, 1);
            }
            else
            {
                EditorGUILayout.TextField("Data", contextObject.ToString());
            }
        }

        private void DrawContextData(Context context, int level)
        {
            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            var contextMemberInfos = ContextTypeCache.GetMemberInfos(context.GetType());
            foreach (var contextMemberInfo in contextMemberInfos)
            {
                if (contextMemberInfo.Property != null)
                {
                    var memberValue = contextMemberInfo.Property.GetValue(context, null);
                    var newMemberValue = this.DrawContextData(contextMemberInfo.Name, memberValue, level);
                    if (contextMemberInfo.Property.CanWrite && !Equals(newMemberValue, memberValue))
                    {
                        contextMemberInfo.Property.SetValue(context, newMemberValue, null);
                    }
                }
            }

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private object DrawContextData(string memberName, object memberValue, int level)
        {
            if (level < MaxLevel)
            {
                var context = memberValue as Context;
                if (context != null)
                {
                    EditorGUILayout.LabelField(memberName, EditorStyles.boldLabel);
                    this.DrawContextData(context, level + 1);
                    return context;
                }

                var collection = memberValue as Collection;
                if (collection != null)
                {
                    EditorGUILayout.LabelField(memberName, EditorStyles.boldLabel);
                    this.DrawContextData(collection, level + 1);
                    return collection;
                }

                var dictionary = memberValue as DataDictionary;
                if (dictionary != null)
                {
                    EditorGUILayout.LabelField(memberName, EditorStyles.boldLabel);
                    this.DrawContextData(dictionary, level + 1);
                    return dictionary;
                }
            }

            // Draw data trigger.
            var dataTrigger = memberValue as DataTrigger;
            if (dataTrigger != null)
            {
                DrawDataTrigger(memberName, dataTrigger);
                return dataTrigger;
            }

            if (memberValue is int)
            {
                return EditorGUILayout.IntField(memberName, (int)memberValue);
            }

            if (memberValue is long)
            {
                return EditorGUILayout.LongField(memberName, (long)memberValue);
            }

            if (memberValue is float)
            {
                return EditorGUILayout.FloatField(memberName, (float)memberValue);
            }

            if (memberValue is bool)
            {
                return EditorGUILayout.Toggle(memberName, (bool)memberValue);
            }

            if (memberValue is string)
            {
                return EditorGUILayout.TextField(memberName, (string)memberValue);
            }

            EditorGUILayout.TextField(memberName, memberValue != null ? memberValue.ToString() : "null");
            return memberValue;
        }

        private void DrawContextData(Collection collection, int level)
        {
            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            var index = 0;
            foreach (var item in collection)
            {
                this.DrawContextData("Item " + index, item, level);
                ++index;
            }

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private Dictionary<DataDictionary, object> newKeys = new Dictionary<DataDictionary, object>(); 

        private void DrawContextData(DataDictionary dataDictionary, int level)
        {
            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            Dictionary<object, object> changedValues = null;
            foreach (var key in dataDictionary.Keys)
            {
                var value = dataDictionary[key];
                var newValue = this.DrawContextData("Item " + key, value, level);
                if (!Equals(value, newValue))
                {
                    if (changedValues == null)
                    {
                        changedValues = new Dictionary<object, object>();
                    }
                    changedValues[key] = newValue;
                }
            }

            if (changedValues != null)
            {
                foreach (var key in changedValues.Keys)
                {
                    dataDictionary[key] = changedValues[key];
                }
            }

            GUILayout.BeginHorizontal();

            object newKey;
            this.newKeys.TryGetValue(dataDictionary, out newKey);
            var keyType = dataDictionary.KeyType;
            const string NewKeyLabel = "New:";
            if (keyType == typeof(string))
            {
                this.newKeys[dataDictionary] = newKey = EditorGUILayout.TextField(NewKeyLabel, (string)newKey);
            }
            else if (keyType.IsEnum)
            {
                if (newKey == null)
                {
                    newKey = Enum.GetValues(keyType).GetValue(0);
                }

                this.newKeys[dataDictionary] = newKey = EditorGUILayout.EnumPopup(NewKeyLabel, (Enum)newKey);
            }

            if (GUILayout.Button("+") && newKey != null)
            {
                var valueType = dataDictionary.ValueType;
                dataDictionary.Add(newKey, valueType.IsValueType ? Activator.CreateInstance(valueType) : null);
            }
            GUILayout.EndHorizontal();

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private static void DrawDataTrigger(string memberName, DataTrigger dataTrigger)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(memberName);
            if (GUILayout.Button("Invoke"))
            {
                dataTrigger.Invoke();
            }
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}