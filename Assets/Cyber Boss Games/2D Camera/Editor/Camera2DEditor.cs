using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Camera2D))]
public class Camera2DEditor : Editor
{
    // SerializedProperty multiTargetAutoZoomDampeningProperty;

    float multiTargetAutoZoomDampening = 1.5f;
    float multiTargetDampening = 3.0f;

    float followDampeningX = 0.4f;
    float followDampeningY = 0.6f;

    float lookAheadAmount = 1.0f;

    float minCameraZoomVal;
    float minCameraZoomLimit = 1;
    float maxCameraZoomVal;
    float maxCameraZoomLimit = 10;

    void OnEnable() {
        minCameraZoomVal = serializedObject.FindProperty("minZoom").floatValue;
        maxCameraZoomVal = serializedObject.FindProperty("maxZoom").floatValue;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targets"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Multi-Target", EditorStyles.boldLabel);
        multiTargetAutoZoomDampening = EditorGUILayout.Slider("Multi-Target Auto Zoom Dampening", multiTargetAutoZoomDampening, 0.1f, 10f);
        serializedObject.FindProperty("multiTargetAutoZoomDampening").floatValue = multiTargetAutoZoomDampening;
        multiTargetDampening = EditorGUILayout.Slider("Multi-Target Dampening", multiTargetDampening, 0.1f, 10f);
        serializedObject.FindProperty("multiTargetDampening").floatValue = multiTargetDampening;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Follow Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetOffset"));
        followDampeningX = EditorGUILayout.Slider("Follow Dampening X", followDampeningX, 0.1f, 10);
        serializedObject.FindProperty("followDampeningX").floatValue = followDampeningX;
        followDampeningY = EditorGUILayout.Slider("Follow Dampening Y", followDampeningY, 0.1f, 10);
        serializedObject.FindProperty("followDampeningY").floatValue = followDampeningY;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Camera Zoom Limiter", EditorStyles.boldLabel);
        minCameraZoomVal = EditorGUILayout.FloatField("Min", minCameraZoomVal);
        maxCameraZoomVal = EditorGUILayout.FloatField("Max", maxCameraZoomVal);
        EditorGUILayout.MinMaxSlider(ref minCameraZoomVal, ref maxCameraZoomVal, minCameraZoomLimit, maxCameraZoomLimit);
        serializedObject.FindProperty("minZoom").floatValue = minCameraZoomVal;
        serializedObject.FindProperty("maxZoom").floatValue = maxCameraZoomVal;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Floor/Ceiling Limiter", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("floorStopLimit"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ceilingStopLimit"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Look Ahead", EditorStyles.boldLabel);
        lookAheadAmount = EditorGUILayout.Slider(lookAheadAmount, 0.4f, 2.5f);
        serializedObject.FindProperty("lookAheadAmount").floatValue = lookAheadAmount;

        EditorGUILayout.Space();
        if (GUILayout.Button("Reset Defaults")) {
            multiTargetAutoZoomDampening = 1.5f;
            followDampeningX = 0.4f;
            followDampeningY = 0.6f;
            multiTargetDampening = 3.0f;
            lookAheadAmount = 1.0f;
            minCameraZoomVal = 1;
            maxCameraZoomVal = 10;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
