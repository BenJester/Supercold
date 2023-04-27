using UnityEditor;
using UnityEngine;

public static class Camera2DTargetsEditorList {
	
    private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

	public static void Show (SerializedProperty list) {
        EditorGUILayout.PropertyField(list);
        // EditorGUILayout.LabelField("Targets", EditorStyles.boldLabel);
		// for (int i = 0; i < list.arraySize; i++) {
        //     EditorGUILayout.BeginHorizontal();
		// 	EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        //     GUI.color = Color.red;
        //     if (GUILayout.Button("-", EditorStyles.miniButtonRight, miniButtonWidth)) {
        //         int oldSize = list.arraySize;
        //         list.DeleteArrayElementAtIndex(i);
        //         if (list.arraySize == oldSize) {
        //             list.DeleteArrayElementAtIndex(i);
        //         }
        //     }
        //     GUI.color = Color.white;
        //     EditorGUILayout.EndHorizontal();
		// }
	}
}