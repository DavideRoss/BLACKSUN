using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JobsManager))]
public class JobsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Unlock New Job")) JobsManager.Instance.UnlockNewJob();
    }
}