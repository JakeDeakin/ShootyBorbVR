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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetLeftFireInputDown()
    {
        return leftController.activateAction.action.ReadValue<float>() > 0f;
    }

    public bool GetRightFireInputDown()
    {
        return rightController.activateAction.action.ReadValue<float>() > 0f;
    }
}
