using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scr_Step : MonoBehaviour
{
    [SerializeField]
    UnityEvent onStepStart;

    [SerializeField]
    UnityEvent onStepEnd;
  
    public void StartStep()
    {
        onStepStart.Invoke();
    }

    public void EndStep()
    {
        onStepEnd.Invoke();
    }

}
