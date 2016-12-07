using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        Debug.logger.Log("trig "+ collision.gameObject.name+"  "+ collision.gameObject.tag);
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.transform.position = new Vector3(0f, -1000f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.logger.Log("collide");
        if(collision.gameObject.name == "Bullet")
        {
            collision.gameObject.transform.position.Set(0f, -1000f, 0f);
        }
    }
}
