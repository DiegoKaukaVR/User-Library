using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotsBateaValidosInstrumentacion : MonoBehaviour
{
    [Header("Objetos validos segun indice 1 o 2")]
    public int indicePos;
    public bool validoSec;
    public int segundoIndice;

    [Header("Guias")]
    public GameObject[] guia;
    public Transform pivotSlot;

    public Material guiaMaterialOn;
    public Material guiaMaterialOff;
    public bool materialesOff;

    [Space(10)]
    public bool handTriggering;

    [Space(10)]
    public bool espacioOcupado;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("RightHand"))
        {
            handTriggering = true;
            DatosImportantes.rightHandIsTriggering = true;
        }
        if (other.gameObject.CompareTag("LeftHand"))
        {
            handTriggering = true;
            DatosImportantes.leftHandIsTriggering = true;
        }

        if (handTriggering)
        {
            if (other.gameObject.GetComponent<tipoDeInstrumento>())
            {
                cambiarMaterialSlot();
                // bool de tipoDeInstrumento.objetoColocadoEnMesa es cuando toca el trigger no cuando esta colocado en si
                other.gameObject.GetComponent<tipoDeInstrumento>().objetoColocadoEnMesa = true;
                if (!other.gameObject.GetComponent<tipoDeInstrumento>().enSlot)
                {
                    guia[other.gameObject.GetComponent<tipoDeInstrumento>().indiceObjeto].SetActive(true);
                }

                if (other.gameObject.GetComponent<tipoDeInstrumento>().transform.position == pivotSlot.position || other.gameObject.GetComponent<tipoDeInstrumento>().enSlot)
                {
                    guia[other.gameObject.GetComponent<tipoDeInstrumento>().indiceObjeto].SetActive(false);
                    espacioOcupado = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<tipoDeInstrumento>())
        {
            // bool de tipoDeObjeto.objetoColocadoEnMesa es cuando toca el trigger no cuando esta colocado en si
            other.gameObject.GetComponent<tipoDeInstrumento>().objetoColocadoEnMesa = false;

            guia[other.gameObject.GetComponent<tipoDeInstrumento>().indiceObjeto].SetActive(false);
            
            espacioOcupado = false;
        }
        if (other.gameObject.CompareTag("RightHand"))
        {
            handTriggering = false;
            DatosImportantes.rightHandIsTriggering = false;
        }
        if (other.gameObject.CompareTag("LeftHand"))
        {
            handTriggering = false;
            DatosImportantes.leftHandIsTriggering = false;
        }

    }
    void cambiarMaterialSlot()
    {
        if (materialesOff)
        {
            foreach (var item in guia)
            {
                item.gameObject.GetComponent<MeshRenderer>().material = guiaMaterialOff;
            }
        }
        else
        {
            foreach (var item in guia)
            {
                item.gameObject.GetComponent<MeshRenderer>().material = guiaMaterialOn;
            }
        }
    }
}
