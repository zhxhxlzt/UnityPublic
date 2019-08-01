using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LongPressToggle))]
public class LongPressToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var t = (LongPressToggle)target;
        t.holdTimeLimit = EditorGUILayout.FloatField("holdTimeLimit", t.holdTimeLimit);
        base.OnInspectorGUI();
    }
}
