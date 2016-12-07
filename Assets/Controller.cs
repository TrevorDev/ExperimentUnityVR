using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller {
    private SteamVR_TrackedController c;

    public bool actionPressed = false;
    public bool actionHeld = false;

    public Transform transform
    {
        get { return c.transform; }
    }

    public Controller(SteamVR_TrackedController c)
    {
        this.c = c;
    }

    private bool prevActionHeldState = false;
    public void Update()
    {
        actionPressed = false;
        prevActionHeldState = actionHeld;
        actionHeld = c.triggerPressed;
        if (actionHeld && !prevActionHeldState)
        {
            actionPressed = true;
        }
    }


}
