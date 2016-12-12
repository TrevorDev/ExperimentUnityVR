using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public int health = 5;
    public string type = "Bullet";
    public bool isInner = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.logger.Log("trig " + collision.gameObject.name + "  " + collision.gameObject.tag);

        if (isInner && this.gameObject.transform.parent.gameObject.GetComponent<Target>().outer.GetComponent<Brick>().health > 0)
        {
            return;
        }

        if (collision.gameObject.tag == this.type)
        {
            health--;
            if(collision.gameObject.tag == "Bullet")
            {
                this.gameObject.transform.localScale *= 0.9f;
            }
            var explodePower = 10;
            if(health <= 0)
            {
                this.transform.position = new Vector3(0f, -1000f, 0f);
                explodePower = 25;
            }

            for (var i = 0; i < explodePower; i++)
            {
                var p = MainScript.global.particleSpawner.createObj(collision.gameObject.transform.position, new Quaternion());
                p.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                p.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
                var offset = 1.5f;
                p.GetComponent<Rigidbody>().velocity = (collision.gameObject.transform.forward * -5) + new Vector3(Random.Range(-offset, offset), 5f + Random.Range(-offset, offset), Random.Range(-offset, offset));
            }
            var newV = this.gameObject.transform.parent.gameObject.GetComponent<Target>().GetComponent<Rigidbody>().velocity;
            newV.z = 2;
            if (collision.gameObject.tag == "Bullet")
            {
                newV = collision.gameObject.GetComponent<Rigidbody>().velocity;
                newV.Scale(new Vector3(1, 0, 1));
                newV = newV.normalized * 2;

            }
            
            this.gameObject.transform.parent.gameObject.GetComponent<Target>().GetComponent<Rigidbody>().velocity = newV;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.transform.position = new Vector3(0f, -10000f, 0f);
        }
    }
}
