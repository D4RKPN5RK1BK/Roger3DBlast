using System.Collections.Generic;
using UnityEngine;

public class Roaming : BaseRouting
{
    
    public List<Vector3> PathPoints = new List<Vector3>();
    public int Target;
    public override Vector3 CurrentTarget {
        get => PathPoints[Target] + transform.position;
    }



    public override void NextTarget()
    {
        Target = Target + 1 < PathPoints.Count ? Target + 1 : 0; 
    }
    
    void Awake() {
        if (PathPoints.Count == 0) {
            PathPoints.Add(Vector3.zero);
        }
    }
}