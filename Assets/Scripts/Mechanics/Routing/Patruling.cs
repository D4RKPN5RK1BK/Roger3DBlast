using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * Тот же класс что и Routing но с двумя точками
 * Смысл создавать так как для всех эле ментов потрулирования есть свой едитор скрипт
 * И там слишком много if else ов.
 **/
public class Patruling : BaseRouting
{
    public Vector3 StartPosition;
    public Vector3 EndPosition;
}
