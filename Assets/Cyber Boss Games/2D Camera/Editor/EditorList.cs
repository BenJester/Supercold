using UnityEditor;
using UnityEngine;

public static class EditorList {
	
    private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

	public static void Show (SerializedProperty list) {
        // EditorGUILayout.PropertyField(list);
        EditorGUILayout.LabelField("Anchors", EditorStyles.boldLabel);
		for (int i = 0; i < list.arraySize; i++) {
            EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            GUI.color = Color.red;
            if (GUILayout.Button("-", miniButtonWidth)) {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(i);
                if (list.arraySize == oldSize) {
                    list.DeleteArrayElementAtIndex(i);
                }
            }
            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
		}
	}
}