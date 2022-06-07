using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    // [SerializeField]
    public GameObject ObjectPrefab;
    
    // [SerializeField]
    public string Tag;
    
    // [SerializeField]
    public int StartQuantity;

    // [SerializeField]
    public Stack<GameObject> Objects;
}
