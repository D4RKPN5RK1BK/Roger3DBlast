using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticiresController : MonoBehaviour
{

    private Rigidbody rigitbody;
    private PoolController poolController;
    public string PoolTag = "Untaged";
    public float WaitUntilDisapear;

    void Awake()
    {
        rigitbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        poolController = FindObjectOfType<PoolController>();
        
    }

    void OnEnable()
    {
        rigitbody.AddForce(new Vector3(0, 10, 0));
        StartCoroutine(SuspectionCoroutine());
    }


    IEnumerator SuspectionCoroutine()
    {
        yield return new WaitForSeconds(WaitUntilDisapear);
        Debug.Log("Предмет должен исчезнуть");
        Disable();
    }

    public void Acitivate(Vector3 position, string poolTag)
    {
        transform.position = position;
        PoolTag = poolTag;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        poolController.Return(PoolTag, gameObject);
    }


}
