using UnityEditor;
using UnityEngine;

public class StandEditor : Editor
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void OnDrawGizmo(Standing standing, GizmoType type)
    {
        var camera = SceneView.lastActiveSceneView.camera.transform.position;
        Vector3 point = standing.Target;
        Handles.color = Color.yellow;
        var pointPosision = standing.transform.TransformPoint(point);
        Handles.DrawSolidDisc(pointPosision, (pointPosision - camera).normalized, 0.1f);
    }
}
