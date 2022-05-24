using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Roaming))]
public class PathEditor : Editor
{
    public void OnSceneGUI()
    {
        var roam = target as Roaming;
        using(var cc = new EditorGUI.ChangeCheckScope()) {
            Vector3[] points = new Vector3[roam.PathPoints.Count];
            for(int i = 0; i < roam.PathPoints.Count; i++) {
                points[i] = roam.transform.InverseTransformPoint(Handles.PositionHandle(roam.transform.TransformPoint(roam.PathPoints[i]),roam.transform.rotation));
            }

            if (cc.changed) {
                for(int i = 0; i < roam.PathPoints.Count; i++) {
                    roam.PathPoints[i] = points[i];
                }
            }
        } 
        // Handles.Label(roam.transform.position, ())
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void OnDrawGizmo(Roaming roaming, GizmoType type)
    {

        var camera = SceneView.lastActiveSceneView.camera.transform.position;
        Vector3[] points = roaming.PathPoints.ToArray();
        Handles.color = Color.yellow;
        foreach (var d in points)
        {
            var pointPosision = roaming.transform.TransformPoint(d);
            Handles.DrawSolidDisc(pointPosision, (pointPosision - camera).normalized, 0.1f);
        }


        if (points.Length > 2)
        {
            Vector3 previous = points[points.Length - 1];
            for (int i = 0; i < points.Length; i++)
            {
                Handles.DrawDottedLine(roaming.transform.TransformPoint(previous), roaming.transform.TransformPoint(points[i]), 5);
                previous = points[i];
            }
        }
        else if (points.Length > 1)
        {
            Handles.DrawDottedLine(roaming.transform.TransformPoint(points[0]), roaming.transform.TransformPoint(points[1]), 5);
        }


    }
}
