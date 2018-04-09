using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount = 10;
    List<GameObject> pooledObjects;


    private void Awake()
    {
        current = this;
    }
	void Start () {
        pooledObjects = new List<GameObject>();
        for (int i=0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}
	
	public GameObject getPooledObject()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
