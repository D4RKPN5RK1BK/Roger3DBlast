/**
 *  Определяет реакцию объекта на получение урона
 *  
 *  Как и у IHitBox имеет метод Responce. А значит 
 *  один объект не может имплементировать сразу оба свойства
 * 
 * 
 **/

public interface IHurtResponce {
    public bool CheckHit(DamageData data);
    public void Responce();
}