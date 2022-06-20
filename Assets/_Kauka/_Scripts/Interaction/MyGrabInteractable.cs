using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabInteractable : XRGrabInteractable
{
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isGrabbed;
    }
    
}
