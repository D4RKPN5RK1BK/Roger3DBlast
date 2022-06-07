using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 *  Контроллер пулов 
 *  
 *  Под пулами подразумеваются группы объектов которые 
 *  могут быть использованы другими контроллерами
 * */

public class PoolController : MonoBehaviour
{
    // Объект для заполнения контроллера из инспектора
    [NonReorderable]
    public Pool[] Pools;

    // Сами пулы
    private Dictionary<string, Stack<GameObject>> activePools;

    // Инициализация
    void Awake()
    {
        activePools = new Dictionary<string, Stack<GameObject>>();
        foreach (Pool p in Pools)
        {
            activePools.Add(p.Tag, new Stack<GameObject>());
            for (int i = 0; i < p.StartQuantity; i++)
            {
                GameObject temp = Instantiate(p.ObjectPrefab, Vector3.zero, new Quaternion(), transform);
                temp.SetActive(false);
                activePools[p.Tag].Push(temp);
            }
        }
    }

    ///<summary>
    /// Берет элемент из пула елментов по укащаному тегу
    ///</summary>
    public GameObject Take(string tag)
    {
        return activePools[tag].Pop();
    }

    ///<summary>
    /// Берет элементы из пула елментов по укащаному тегу
    ///</summary>
    public IEnumerable<GameObject> Take(string tag, int count)
    {
        List<GameObject> list = new List<GameObject>();

        if (count >= activePools[tag].Count)
            for (int i = 0; i < count; i++)
            {
                list.Add(activePools[tag].Pop());
            }

        for (int i = 0; i < count; i++)
        {
            list.Add(activePools[tag].Pop());
        }
        return list;
    }


    ///<summary>
    /// Возвращает элемент в стек с указанным тегом
    ///</summary>
    public void Return(string tag, GameObject element)
    {
        activePools[tag].Push(element);
    }

    ///<summary>
    /// Возвращает элементы в стек с указанным тегом
    ///</summary>
    public void Return(string tag, IEnumerable<GameObject> elements)
    {
        foreach (var e in elements)
        {
            activePools[tag].Push(e);
        }
    }

}
