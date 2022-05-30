/**
 *  Интерфейс для всех хитбоксов в игре
 * 
 * 
 **/

public interface IHitBox {
    public IHitResponce HitResponce { get; set; }
    public void CheckHit();
}