using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {
    public SteamVR_Controller.Device controller;

    public delegate void ReadyAction();
    public event ReadyAction OnReady;
    // Use this for initialization
    void Start () {
        controller = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        Debug.logger.Log("hit");
        OnReady();
    }
	
	// Update is called once per frame
	void Update () {
	}
}
