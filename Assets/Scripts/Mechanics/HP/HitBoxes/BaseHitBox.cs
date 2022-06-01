using UnityEngine;

public abstract class BaseHitBox : MonoBehaviour
{
    public abstract void StartAtack();
    public abstract void EndAtack();
    public abstract void HitHandler();
}