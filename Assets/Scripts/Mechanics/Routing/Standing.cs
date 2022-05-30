using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standing : BaseRouting
{
    public Vector3 Target;

    public override Vector3 CurrentTarget {
        get => Target + transform.position;
    }

    public override void NextTarget()
    {
    }
}
