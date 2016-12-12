using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {
    static public GameObject particle;
    static List<GameObject> particles = new List<GameObject>();
    static int maxParticles = 300;
    static int curCount = 0;
    public static GameObject createParticle(Vector3 position, Quaternion rotation)
    {
        GameObject newParticle;
        if (curCount < maxParticles)
        {
            newParticle = Instantiate(particle, position, rotation);
            particles.Add(newParticle);
        }
        else
        {
            newParticle = particles[curCount];
            newParticle.transform.position = position;
            newParticle.transform.rotation = rotation;
            curCount++;
            curCount = curCount % maxParticles;
        }

        newParticle.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        return newParticle;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
