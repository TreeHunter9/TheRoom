using TheRoom.InteractableObjects;
using UnityEditor;

namespace TheRoom.InteractableObjects.Editor
{
    [CustomEditor(typeof(RotatableObject))]
    public class RotatableObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_simpleRotation"));
            SerializedProperty simpleRotation = serializedObject.FindProperty("_simpleRotation");
            if (simpleRotation.boolValue == true)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_mouseXRotationAxis"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_mouseYRotationAxis"));
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_wrap"));
            SerializedProperty wrap = serializedObject.FindProperty("_wrap");
            if (wrap.boolValue == false)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_minRotation"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_maxRotation"));
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_stopWhenOnPosition"));

            if (simpleRotation.boolValue == false)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_rotationOnAxis"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_speedRotation"));
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_autoMagnet"));
            SerializedProperty autoMagnet = serializedObject.FindProperty("_autoMagnet");
            if (autoMagnet.boolValue == true)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_autoMagnetAngle"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_magnetOffset"));
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_invertX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_invertY"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_invertZ"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_changeRotationX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_changeRotationY"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_changeRotationZ"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_actionOnComplete"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
