using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    //Si se usa tp 
    public bool enableTP;

    public XRController rightTPRay;
    public XRController leftTPRay;
    public InputHelpers.Button teleportActivateButton;
    public float activationStart = 0.1f;

    private void Start()
    {
        rightTPRay.gameObject.SetActive(false);
        leftTPRay.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (enableTP)
        {
            if (rightTPRay)
                rightTPRay.gameObject.SetActive(CheckIfActivateTP(rightTPRay));
       
            if (leftTPRay)
                leftTPRay.gameObject.SetActive(CheckIfActivateTP(leftTPRay));
     
        }
    }

    /// <summary>
    /// 
    /// </summary>
  
    public bool CheckIfActivateTP(XRController controller)
    {                      
        // controller lleva el input device
        // si se presiona el boton del device (mando) - boton en concreto  - bool a usar si presionado - si se presiona lo suficiente
        InputHelpers.IsPressed(controller.inputDevice, teleportActivateButton, out bool isActivated, activationStart);
        return isActivated;
    }
}
