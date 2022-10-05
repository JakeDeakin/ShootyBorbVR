using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInputHandler : MonoBehaviour
{
    public ActionBasedController rightController;
    public ActionBasedController leftController;

    private bool leftTriggedWasPressed;
    private bool rightTriggedWasPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetLeftFireInputPressed()
    {
        leftTriggedWasPressed = true;
        return leftController.activateAction.action.triggered;
    }

    public bool GetLeftFireInputHeld()
    {
        leftTriggedWasPressed = true;      
        return leftController.activateAction.action.ReadValue<float>() > 0f;
    }

    public bool GetLeftFireInputReleased()
    {
        if (leftTriggedWasPressed && !GetLeftFireInputHeld() || leftTriggedWasPressed && !GetLeftFireInputHeld())
        {
            leftTriggedWasPressed = false;
            return true;
        }
        return false;
    }

    public bool GetRightFireInputPressed()
    {

        return rightController.activateAction.action.triggered;
    }

    public bool GetRightFireInputHeld()
    {
        rightTriggedWasPressed = true;
        return rightController.activateAction.action.ReadValue<float>() > 0f;
    }

    public bool GetRightFireInputReleased()
    {
        if (rightTriggedWasPressed && !GetRightFireInputHeld() || rightTriggedWasPressed && !GetRightFireInputHeld())
        {
            rightTriggedWasPressed = false;
            return true;
        }
        return false;
    }
}
