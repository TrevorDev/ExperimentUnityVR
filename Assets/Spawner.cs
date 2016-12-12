using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ScriptableObject {

    public GameObject spawnObj;
    private List<GameObject> objs = new List<GameObject>();
    private int curObj = 0;
    private int maxObj = 300;
    public GameObject createObj(Vector3 position, Quaternion rotation)
    {
        GameObject newObj;
        if (objs.Count < maxObj)
        {
            newObj = Instantiate(spawnObj, position, rotation);
            objs.Add(newObj);
        }
        else
        {
            newObj = objs[curObj];
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
            curObj++;
            curObj = curObj % maxObj;
        }

        newObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        return newObj;
    }

    public Spawner(GameObject spawnerType, int maxCount)
    {
        this.spawnObj = spawnerType;
        this.maxObj = maxCount;
    }
}
