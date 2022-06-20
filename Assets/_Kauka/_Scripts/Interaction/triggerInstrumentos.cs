using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class triggerInstrumentos : MonoBehaviour
{    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.parent.GetComponent<tipoDeInstrumento>())
        {
            // detectar el primer hijo
            //Debug.Log(other.gameObject.transform.GetChild(0).transform.name);
            if (other.gameObject.transform.parent.GetComponent<tipoDeInstrumento>().objetoEnMano)
            {
                if (other.gameObject.transform.parent.GetChild(0).Find("CanvasImagen"))
                {
                    other.gameObject.transform.parent.GetChild(0).Find("CanvasImagen").GetComponent<Animator>().SetBool("open", true);
                }   
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.GetComponent<tipoDeInstrumento>())
        {
            if (other.gameObject.transform.parent.GetComponent<tipoDeInstrumento>().objetoEnMano)
            {
                if (other.gameObject.transform.parent.GetChild(0).Find("CanvasImagen"))
                {
                    other.gameObject.transform.parent.GetChild(0).Find("CanvasImagen").GetComponent<Animator>().SetBool("open", false);
                }
            }
        }
    }
}
