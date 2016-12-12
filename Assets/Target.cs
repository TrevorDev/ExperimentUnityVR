using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject brick;

    public GameObject outer;
    public GameObject inner;
	// Use this for initialization
	public void Start () {
        this.transform.position =new Vector3(Random.Range(-5, 5), Random.Range(0, 2), 10);
        var scale = 0.70f;
        if(outer == null)
        {
            outer = Instantiate(brick, this.transform.position, this.transform.rotation);
        }
        outer.transform.position = this.transform.position;
        outer.transform.localScale = new Vector3(scale, scale, scale);
        outer.transform.parent = this.gameObject.transform;
        outer.GetComponent<Brick>().type = Random.Range(0.0f, 1.0f) > 0.5 ? "Sword" : "Bullet";
        outer.GetComponent<Brick>().isInner = false;
        if (outer.GetComponent<Brick>().type == "Sword")
        {
            outer.GetComponent<Renderer>().material.color = new Color(206f / 255f, 12.0f / 255f, 44f / 255f, 255f);
        }
        else
        {
            outer.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 255f);
        }
        outer.GetComponent<Brick>().health = 2;

        if (inner == null)
        {
            inner = Instantiate(brick, this.transform.position, this.transform.rotation);
        }
        inner.transform.position = this.transform.position;
        inner.transform.localScale = new Vector3(scale/2, scale/2, scale/2);
        inner.transform.parent = this.gameObject.transform;
        inner.GetComponent<Brick>().type = Random.Range(0.0f, 1.0f) > 0.5 ? "Sword" : "Bullet";
        inner.GetComponent<Brick>().isInner = true;
        if (inner.GetComponent<Brick>().type == "Sword")
        {
            inner.GetComponent<Renderer>().material.color = new Color(206f / 255f, 12.0f / 255f, 44f / 255f, 255f);
        }
        else
        {
            inner.GetComponent<Renderer>().material.color = new Color(1,1,1, 255f);
        }
        
        
        inner.GetComponent<Brick>().health = 1;
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.position = transform.position + new Vector3(0, .005f, 0);
        //Debug.logger.Log(this.GetComponent<Rigidbody>().velocity);
    }
}
