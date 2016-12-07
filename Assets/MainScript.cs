using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

    public GameObject sword;

    //Controller
    public GameObject cameraRig;
    private Controller left;
    private Controller right;

    private List<Controller> controllers = new List<Controller>();
    float swordLen = 0.1f;
    // Use this for initialization
    void Start () {
        var manager = cameraRig.GetComponent<SteamVR_ControllerManager>();
        left = new Controller(cameraRig.GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>());
        right = new Controller(cameraRig.GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>());
        controllers.Add(left);
        controllers.Add(right);

        sword = Instantiate(sword, left.transform);
        sword.transform.localScale = new Vector3(.05f, .05f, swordLen);
    }
	
	// Update is called once per frame
	void Update () {
        controllers.ForEach((c) => { c.Update(); });
        if (left.actionPressed)
        {
            var newBullet = createBullet(left.transform.position, left.transform.rotation);
            newBullet.transform.localScale = new Vector3(.1f, .1f, .1f);
            newBullet.GetComponent<Rigidbody>().velocity = (left.transform.up*-1+ left.transform.forward).normalized * 10;
            swordLen += 0.1f;
            sword.transform.localScale = new Vector3(.05f, .05f, swordLen);
        }

        sword.transform.position = left.transform.position+(left.transform.forward* (swordLen/2+ 0.2f));
        sword.transform.rotation = left.transform.rotation;

        if (right.actionPressed)
        {
            //var gridSize = 5;
            for(var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 7; y++)
                {
                    float offset = (float)x - 5f;
                    var euler = right.transform.rotation.eulerAngles;
                    euler.x = 0;
                    euler.z = 0;
                    var newRotation = Quaternion.Euler(euler);
                    //Debug.logger.Log(euler);
                    var newPosition = right.transform.position;
                    newPosition += newRotation * Vector3.forward;
                    newPosition.y = y * 0.13f + 0.1f;
                    newPosition += offset * 0.13f * (newRotation * Vector3.left);

                    var newBullet = createBullet( newPosition, newRotation);

                    newBullet.transform.localScale = new Vector3(.1f, .1f, .1f);
                }
            }
                
        }
    }

    //block spawner
    public GameObject bullet;
    private List<GameObject> bullets = new List<GameObject>();
    private int curBlock = 0;
    private int maxBlocks = 100;
    private GameObject createBullet(Vector3 position, Quaternion rotation)
    {
        GameObject newBullet;
        if (bullets.Count < maxBlocks)
        {
            newBullet = Instantiate(bullet, position, rotation);
            bullets.Add(newBullet);
        }
        else
        {
            newBullet = bullets[curBlock];
            newBullet.transform.position = position;
            newBullet.transform.rotation = rotation;
            curBlock++;
            curBlock = curBlock % maxBlocks;
        }

        newBullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        return newBullet;
    }
}
