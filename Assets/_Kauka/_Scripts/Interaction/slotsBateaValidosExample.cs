using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotsBateaValidosExample : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("RightHand"))
        {
            DatosImportantes.rightHandIsTriggering = true;
        }
        if (other.gameObject.CompareTag("LeftHand"))
        {
            DatosImportantes.leftHandIsTriggering = true;
        }
        if (DatosImportantes.rightHandIsTriggering || DatosImportantes.leftHandIsTriggering)
        {
            if (other.gameObject.GetComponent<tipoDeObjetoExample>())
            {
                checkSlot();
                // bool de tipoDeObjeto.objetoColocadoEnMesa es cuando toca el trigger no cuando esta colocado en si
                other.gameObject.GetComponent<tipoDeObjetoExample>().objetoColocadoEnMesa = true;
                guia[other.gameObject.GetComponent<tipoDeObjetoExample>().indiceObjeto].SetActive(true);

                if (other.gameObject.GetComponent<tipoDeObjetoExample>().transform.position == pivotSlot.position)
                {
                    guia[other.gameObject.GetComponent<tipoDeObjetoExample>().indiceObjeto].SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<tipoDeObjetoExample>())
        {
            // bool de tipoDeObjeto.objetoColocadoEnMesa es cuando toca el trigger no cuando esta colocado en si
            other.gameObject.GetComponent<tipoDeObjetoExample>().objetoColocadoEnMesa = false;

            guia[other.gameObject.GetComponent<tipoDeObjetoExample>().indiceObjeto].SetActive(false);
        }
        if (other.gameObject.CompareTag("RightHand"))
        {
            DatosImportantes.rightHandIsTriggering = false;
        }
        if (other.gameObject.CompareTag("LeftHand"))
        {
            DatosImportantes.leftHandIsTriggering = false;
        }
    }
    void checkSlot()
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
