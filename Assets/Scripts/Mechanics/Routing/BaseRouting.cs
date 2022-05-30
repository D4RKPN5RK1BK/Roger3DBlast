using UnityEngine;

public abstract class BaseRouting : MonoBehaviour {
    public virtual Vector3 CurrentTarget { get; }

    public abstract void NextTarget();

    public virtual void SelectTarget(int i) {}

    public virtual void ResetTarget() {}
}