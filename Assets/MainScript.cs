using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {
    public static MainScript global;
    public GameObject sword;
    public GameObject target;

    //Controller
    public GameObject cameraRig;
    private Controller left;
    private Controller right;

    private List<Controller> controllers = new List<Controller>();
    float swordLen = 1.1f;

    private List<GameObject> targets = new List<GameObject>();

    public int score = 0;

    //spawners
    public GameObject bullet;
    public GameObject particle;
    public Spawner particleSpawner;
    public Spawner bulletSpawner;

    // Use this for initialization
    void Start () {
        global = this;
        particleSpawner = new Spawner(particle, 300);
        bulletSpawner = new Spawner(bullet, 300);

        var manager = cameraRig.GetComponent<SteamVR_ControllerManager>();
        left = new Controller(manager.left.GetComponent<SteamVR_TrackedController>());
        right = new Controller(manager.right.GetComponent<SteamVR_TrackedController>());

        left.sword = Instantiate(sword, left.transform);
        left.sword.transform.localScale = new Vector3(.05f, .05f, swordLen);

        right.sword = Instantiate(sword, right.transform);
        right.sword.transform.localScale = new Vector3(.05f, .05f, swordLen);

        controllers.Add(left);
        controllers.Add(right);
        SpawnTargets();
    }

	void SpawnTargets()
    {
        if(targets.Count <= 3)
        {
            targets.Add(Instantiate(target));
            Invoke("SpawnTargets", 10);
        }
    }
    // Update is called once per frame
    void Update () {
        //Controlls
        controllers.ForEach((c) => {
            c.Update();
            if (c.actionPressed)
            {
                var newBullet = bulletSpawner.createObj(c.transform.position, c.transform.rotation);
                newBullet.transform.localScale = new Vector3(.1f, .1f, .1f);
                newBullet.GetComponent<Rigidbody>().velocity = (c.transform.up * -1 + c.transform.forward).normalized * 10;
                c.sword.transform.localScale = new Vector3(.05f, .05f, swordLen);
            }

            c.sword.transform.position = c.transform.position + (c.transform.forward * (swordLen / 2 + 0.2f));
            c.sword.transform.rotation = c.transform.rotation;
        });

        //Basic AI
        targets.ForEach((t) => {
            var dir = cameraRig.transform.position - t.transform.position;
            dir.y = 0;
            if (dir.magnitude > 0.5)
            {
                dir.Normalize();
                dir *= 0.1f;
                t.GetComponent<Rigidbody>().velocity += dir;
                if (t.GetComponent<Rigidbody>().velocity.magnitude > 2)
                {
                    t.GetComponent<Rigidbody>().velocity = t.GetComponent<Rigidbody>().velocity.normalized * 2;
                }
            }
            else if(t.GetComponent<Target>().outer.GetComponent<Brick>().health > 0 || t.GetComponent<Target>().inner.GetComponent<Brick>().health > 0)
            {
                if(score != 0)
                {
                    GameObject.Find("Score").GetComponent<TextMesh>().text = "Game Over: " + score;
                }
                score = 0;
                t.GetComponent<Rigidbody>().velocity = new Vector3();
            }

            if (t.GetComponent<Target>().outer.GetComponent<Brick>().health <= 0 && t.GetComponent<Target>().inner.GetComponent<Brick>().health <= 0)
            {
                score++;
                GameObject.Find("Score").GetComponent<TextMesh>().text = ""+ score;
                t.GetComponent<Target>().Start();
            }
        });

        
    }
}
