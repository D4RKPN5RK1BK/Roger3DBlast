using UnityEngine;
/**
 *  Определяет общие данные передаваемые при получении / нанаесении урона.
 *  
 *  Нигде не имплементировано
 **/
public class DamageData
{
    public Vector3 HitPoint;
    public Vector3 HitNormal;
    public IHurtBox HurtBox;
    public IHitBox HitBox;

    public bool Validate()
    {
        if (HurtBox?.CheckHit(this) ?? false)
            if (HurtBox?.HurtResponce?.CheckHit(this) ?? false)
                if (HitBox?.HitResponce?.CheckHit(this) ?? false)
                    return true;

        return false;
    }
}