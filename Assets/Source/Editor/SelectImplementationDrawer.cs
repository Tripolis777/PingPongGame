using System;
using System.Collections.Generic;
using System.Linq;
using Source.Core;
using UnityEditor;
using UnityEngine;

namespace Source.Editor
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : PropertyDrawer
    {
        private static readonly float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
        private static Dictionary<Type, Type[]> implementations = new Dictionary<Type, Type[]>();
        private static Dictionary<Type, string[]> implNames = new Dictionary<Type, string[]>();

        private int selectedImplIndex = -1;
        private bool showCreateSection = false;

        private Type BaseType => (attribute as SelectImplementationAttribute)?.instanceType;
        private float OptionsHeight => lineHeight * (showCreateSection ? 4 : 2) + EditorGUIUtility.standardVerticalSpacing;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, property.isExpanded);
            
            if (property.isExpanded)
                height += OptionsHeight;
            
            return height;
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var headerPosition = new Rect(position);
            headerPosition.size = 16f * Vector2.one;
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(headerPosition, property.isExpanded, label);
            EditorGUI.EndFoldoutHeaderGroup();

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.PropertyField(position, property, GUIContent.none, property.isExpanded);
            DrawCreationSection(position, property);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DrawCreationSection(Rect position, SerializedProperty property)
        {
            if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                selectedImplIndex = 0;
                showCreateSection = true;
                property.isExpanded = true;
            }
            else if (selectedImplIndex == -1)
                selectedImplIndex = TryFindInstanceIndex(property);

            if (!property.isExpanded)
                return;

            var height = EditorGUI.GetPropertyHeight(property, true);
            var separatorRect = new Rect(position.x, position.y + height + EditorGUIUtility.standardVerticalSpacing, position.width + 4f, OptionsHeight);
            var labelRect = new Rect(separatorRect.x + 2f, separatorRect.y + EditorGUIUtility.standardVerticalSpacing, position.width,
                EditorGUIUtility.singleLineHeight);
            var chooseButtonRect = new Rect(labelRect.x, labelRect.y + lineHeight, labelRect.width,
                EditorGUIUtility.singleLineHeight);
            
            EditorGUI.DrawRect(separatorRect, new Color(0f, 1f, 0f, 0.3f));

            GUI.Box(labelRect, "Настройки реализации");
            if (GUI.Button(chooseButtonRect, showCreateSection ? "Hide" : "Change"))
                showCreateSection = !showCreateSection;
            
            if (showCreateSection)
                DrawChooseImplementation(chooseButtonRect, property);
        }

        private void DrawChooseImplementation(Rect position, SerializedProperty property)
        {
            if (BaseType == null)
                return;
            
            Refresh();
            implementations.TryGetValue(BaseType, out var implList);
            if (implList?.Length == 0)
                return;
            
            var selectImplPopupRect = new Rect(position.x, position.y + lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var createButtonRect = new Rect(selectImplPopupRect.x, selectImplPopupRect.y + lineHeight,
                position.width, EditorGUIUtility.singleLineHeight);

            
            selectedImplIndex = EditorGUI.Popup(selectImplPopupRect, "Implementation", selectedImplIndex,
                implNames[BaseType]);

            if (GUI.Button(createButtonRect, "Create"))
                TryCreateInstance(property);
        }
        
        private void TryCreateInstance(SerializedProperty property)
        {
            Refresh();
            implementations.TryGetValue(BaseType, out var implList);
            if (implList == null || implList.Length == 0)
                return;
            
            property.managedReferenceValue = Activator.CreateInstance(implList[selectedImplIndex]);
            property.serializedObject.ApplyModifiedProperties();
        }

        private int TryFindInstanceIndex(SerializedProperty property)
        {
            Refresh();
            var baseType = BaseType;
            if (baseType == null || !implNames.TryGetValue(baseType, out var names))
                return 0;
            
            return Mathf.Max(0, names.IndexOf(x => property.managedReferenceFullTypename.Contains(x)));
        }
        
        private void Refresh()
        {
            var baseType = BaseType;
            if (baseType == null)
                return;
            
            if (implementations.TryGetValue(baseType, out var implList) && implList.Length > 0)
                return;
            
            implList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => baseType.IsAssignableFrom(x) && !x.IsAbstract).OrderByDescending(x => x == baseType).ToArray();
            implementations[baseType] = implList;

            implNames[baseType] = implList.Select(x => x.FullName).ToArray();
        }
    }
}