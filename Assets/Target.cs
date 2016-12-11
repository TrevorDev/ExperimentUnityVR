using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject brick;
	// Use this for initialization
	void Start () {
        var size = 10;
        var scale = 0.05f;
        for(var i = 0; i<size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                for (var k = 0; k < size; k++)
                {
                    var newBrick = Instantiate(brick);
                    newBrick.transform.localScale = new Vector3(scale, scale, scale);
                    newBrick.transform.position = newBrick.transform.position + new Vector3(i * scale, j * scale, k * scale);
                    newBrick.transform.parent = this.gameObject.transform;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.position = transform.position + new Vector3(0, .005f, 0);
    }
}
