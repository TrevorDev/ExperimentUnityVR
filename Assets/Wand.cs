using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {
    private static bool found1 = false;
    public SteamVR_Controller.Device controller;

    public delegate void ReadyAction();
    public event ReadyAction OnReady;
    public int index;
    // Use this for initialization
    void Start () {
        index = (int)GetComponent<SteamVR_TrackedObject>().index;
        Debug.logger.Log(index);
        if (index == 1)
        {
            Invoke("Ready", 3f);
        }else
        {
            Ready();
        }
        
    }

    void Ready()
    {
        Debug.logger.Log("ready "+ (int)GetComponent<SteamVR_TrackedObject>().index);
        controller = SteamVR_Controller.Input(index);
        OnReady();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.logger.Log("boom " + (int)GetComponent<SteamVR_TrackedObject>().index);
        //Debug.logger.Log((int)GetComponent<SteamVR_TrackedObject>().index);
    }
}
