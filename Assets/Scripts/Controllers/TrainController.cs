using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [Min(0)]
    public float CarriageDistance = 1;
    private List<GameObject> carriages;

    void Awake()
    {
        carriages = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCarriage(CarriageController carriage)
    {
        if (carriages.Count == 0)
        {
            carriage.SetFolowingObject(this.gameObject);
        }
        else
        {
            carriage.SetFolowingObject(carriages[carriages.Count - 1]);

        }
        carriages.Add(carriage.gameObject);
    }

    public void GetLastComponet()
    {

    }

    public void BindComponent(CarriageController carriage)
    {

    }
}
