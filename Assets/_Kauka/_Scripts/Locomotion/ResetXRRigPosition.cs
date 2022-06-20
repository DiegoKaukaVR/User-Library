using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This class is used to reset position of XR Rig via device 
/// </summary>
public class ResetXRRigPosition : MonoBehaviour
{
    InputDevice thisDevice;
    public GameObject userMenu;

    private void Start()
    {
        TakeDevice();    
    }
    private void Update()
    {
        if(!thisDevice.isValid)
        {
            TakeDevice();
        }
    }
    void TakeDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // manera mas larga y precisa de coger control de un device
        InputDevices.GetDevices(devices);

        if (devices.Count > 0)
        {
            thisDevice = devices[0];
            Debug.Log(thisDevice.name);
        }
    }

    public void ResetPosition()
    {
        StartCoroutine(CloseUserMenuCor());
    }
    IEnumerator CloseUserMenuCor()
    {
        yield return new WaitForSeconds(1f);
        thisDevice.subsystem.TryRecenter();
        userMenu.SetActive(false);
    }
}
