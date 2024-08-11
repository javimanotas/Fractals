using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class HierarchyToggle
{
    static HierarchyToggle()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    static void OnHierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

        if (gameObject != null)
        {
            var rect = new Rect(selectionRect);
            rect.x -= 27;
            rect.width = 13;

            var isActive = EditorGUI.Toggle(rect, gameObject.activeSelf);

            if (isActive != gameObject.activeSelf)
            {
                Undo.RecordObject(gameObject, "Changing active state of a game object");
                gameObject.SetActive(isActive);
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
        }
    }
}