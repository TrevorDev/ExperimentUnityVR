using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {
    public GameObject cameraRig;
    private GameObject leftGameObj;
    private GameObject rightGameObj;

    public GameObject cube;

    private SteamVR_TrackedController left;
    private SteamVR_TrackedController right;

    private List<GameObject> blocks = new List<GameObject>();
    private int curBlock = 0;
    private int maxBlocks = 100;
    private GameObject createCube(Vector3 position, Quaternion rotation)
    {
        GameObject newCube;
        if (blocks.Count < maxBlocks)
        {
            newCube = Instantiate(cube, position, rotation);
            blocks.Add(newCube);
        }
        else
        {
            newCube = blocks[curBlock];
            newCube.transform.position = position;
            newCube.transform.rotation = rotation;
            curBlock++;
            curBlock = curBlock % maxBlocks;
        }

        newCube.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        return newCube;
    }


    // Use this for initialization
    void Start () {
        var manager = cameraRig.GetComponent<SteamVR_ControllerManager>();
        leftGameObj = manager.left;
        rightGameObj = manager.right;
        left = cameraRig.GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
        right = cameraRig.GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (left != null)
        {
            //Debug.logger.Log(left.GetHairTrigger());
            if (left.triggerPressed)
            {
                Debug.logger.Log("created");
                var newCube = createCube(leftGameObj.transform.position, leftGameObj.transform.rotation);
                newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
                newCube.GetComponent<Rigidbody>().velocity = leftGameObj.transform.forward * 20;
            }
        }

        

        if (right != null)
        {
            if (right.triggerPressed)
            {
                Debug.logger.Log("created");
                //var gridSize = 5;
                for(var x = 0; x < 2; x++)
                {
                    for (var y = 0; y < 10; y++)
                    {
                        float offset = (float)x - 2.5f;
                        var euler = rightGameObj.transform.rotation.eulerAngles;
                        euler.x = 0;
                        euler.z = 0;
                        var newRotation = Quaternion.Euler(euler);
                        //Debug.logger.Log(euler);
                        var newPosition = rightGameObj.transform.position;
                        newPosition += newRotation * Vector3.forward;
                        newPosition.y = y * 0.1f;
                        newPosition += offset * 0.1f * (newRotation * Vector3.left);

                        var newCube = createCube( newPosition, newRotation);
                        newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
                    }
                }
                
            }
        }
    }
}
