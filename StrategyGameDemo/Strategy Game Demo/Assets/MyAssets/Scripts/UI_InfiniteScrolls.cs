using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InfiniteScrolls : MonoBehaviour
{
    private Queue<GameObject> objectPool;
    [SerializeField] private GameObject[] buildings;
    [SerializeField] private int poolSizePerObject;

    private GameObject currentObj;


    void Awake()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSizePerObject; i++)
        {
            for (int j = 0; j < buildings.Length; j++)
            {
                GameObject obj = Instantiate(buildings[j], this.gameObject.transform);

                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }
        }

        currentObj = objectPool.Dequeue();

        currentObj.SetActive(true);

        objectPool.Enqueue(currentObj);
    }

    public void SwitchPooledObject()
    {
        currentObj.SetActive(false);

        GameObject obj = objectPool.Dequeue();

        obj.SetActive(true);

        currentObj = obj;

        objectPool.Enqueue(obj);
    }
}
