using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {
    //public SteamVR_Controller.Device controller;

    public delegate void ReadyAction();
    public event ReadyAction OnReady;
    // Use this for initialization
    void Start () {
        Debug.logger.Log("WAND created");
    }

    public SteamVR_Controller.Device GetController()
    {
        var x = (int)GetComponent<SteamVR_TrackedObject>().index;
        Debug.logger.Log(x);
        if (x == -1)
        {
            return null;
        }
        return SteamVR_Controller.Input(x);
    }
	
	// Update is called once per frame
	void Update () {
    }
}
