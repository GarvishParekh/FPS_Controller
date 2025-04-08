#if UNITY_EDITOR
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

[CustomEditor(typeof(CanvasIdentity))]
public class CanvasIdentityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CanvasIdentity identity = (CanvasIdentity)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("database"));

        if (identity.database != null)
        {
            string[] options = identity.database.canvasIdentities.ToArray();
            identity.selectedIndex = EditorGUILayout.Popup("Canvas Identity", identity.selectedIndex, options);
        }

        serializedObject.ApplyModifiedProperties();
    }    
}
#endif
