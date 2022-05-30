/**
 *  Определяет реакцию на нанесение урона.
 * 
 * 
 **/

public interface IHitResponce {
    public bool CheckHit(DamageData data);
    public void Responce();
}