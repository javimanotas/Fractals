#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[InitializeOnLoad]
public static class HierarchyIconDisplay
{
    static bool _hierarchyHasFocus = false;

    static EditorWindow _hierarchyWindow;

    static HierarchyIconDisplay()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        EditorApplication.update += OnUpdate;
    }

    static void OnHierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
        
        if (obj == null)
        {
            return;
        }

        var components = obj.GetComponents<Component>();

        if (components == null || components.Length == 0)
        {
            return;
        }

        Component component;

        if (components.Length == 1)
        {
            component = components[0];
        }
        else
        {
            component = components[1];

            if (component is CanvasRenderer && components.Length > 2)
            {
                component = components[2];
            }
        }
        
        var type = component.GetType();
        var content = EditorGUIUtility.ObjectContent(component, type);
        content.text = null;
        content.tooltip = type.Name;

        if (content.image == null)
        {
            return;
        }

        var color = BackgroundColorHelper.Get(
            Selection.instanceIDs.Contains(instanceId),
            selectionRect.Contains(Event.current.mousePosition),
            _hierarchyHasFocus
        );

        var bgRect = selectionRect;
        bgRect.width = 18.5f;
        EditorGUI.DrawRect(bgRect, color);

        EditorGUI.LabelField(selectionRect, content);
    }

    static void OnUpdate()
    {
        if (_hierarchyWindow == null)
        {
            var type = Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor");
            _hierarchyWindow = EditorWindow.GetWindow(type);
        }

        _hierarchyHasFocus = EditorWindow.focusedWindow != null && EditorWindow.focusedWindow == _hierarchyWindow;
    }
}
#endif