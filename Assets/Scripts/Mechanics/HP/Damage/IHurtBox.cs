using UnityEngine;

/**
 *  Интерфейс реализуемый во всех hurtBox'ах в игре
 * 
 * 
 **/

public interface IHurtBox {
    public GameObject Owner { get; }
    public Transform transform { get; }
    public IHurtResponce HurtResponce { get; set; }

    public bool CheckHit(DamageData data);
}