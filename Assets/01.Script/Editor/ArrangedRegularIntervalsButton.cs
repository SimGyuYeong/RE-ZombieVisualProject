using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArrangedRegularIntervals))]
public class ArrangedRegularIntervalsButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ArrangedRegularIntervals generator = (ArrangedRegularIntervals)target;
        if (GUILayout.Button("Generate"))
        {
            generator.SetPos();
        }
    }
}
