using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInputs : MonoBehaviour
{
    public bool Apressed = false;
    public bool Wpressed = false;
    public bool Dpressed = false;
    public bool spacePressed = false;
    public bool shiftPressed = false;
    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;

    public void ResetMovement()
    {
        Apressed = false;
        Dpressed = false;
        spacePressed = false;
    }

    public void ResetEcho()
    {
        Wpressed = false;
        shiftPressed = false;
    }
}
