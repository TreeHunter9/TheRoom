using UnityEditor;

namespace Interactable_object.Editor
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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_fromRotation"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_toRotation"));
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_stopWhenOnPosition"));
            
            if(simpleRotation.boolValue == false) 
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_rotationOnAxis"));
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_actionOnComplete"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
