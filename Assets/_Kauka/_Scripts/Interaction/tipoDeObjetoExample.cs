using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipoDeObjetoExample : MonoBehaviour
{
    [Header("Tipo y comprobacion")]
    [Space(10)]
    public primitives primitives;

    // indice de objeto
    [Tooltip("Esto se usa para comprobar si el objeto es correcto en el slot colocado")]
    public int indiceObjeto;

    // child de interactableGroup
    public Transform interactableGroup;
    Transform tranformSlot;

    // bool slot checks
    bool slotOcupado;
    bool dentroDeTrigger;

    // para que no añada dos instrumentos a la lista -> checkInstrumentosNecesarios script
    public bool objetoAñadidoEnLista;
    public bool objetoColocadoEnMesa;

    // Opcion correcta (no puede ser estatica, a menos que se haga una bool para cada instrumento)
    public bool objetoCorrecto;
    public GameObject flechaPosicionInstrumento;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InstrumentosTrigger"))
        {
            slotOcupado = true;
        }
        if (other.gameObject.GetComponent<slotsBateaValidosExample>())
        {
            if (!slotOcupado)
            {
                other.gameObject.GetComponent<slotsBateaValidosExample>().materialesOff = false;

                if (DatosImportantes.rightHandIsTriggering || DatosImportantes.leftHandIsTriggering)
                {
                    dentroDeTrigger = true;

                    tranformSlot = other.gameObject.transform.parent.transform;
                    //slot correcto con 1 o 2 opciones
                    if (other.gameObject.GetComponent<slotsBateaValidosExample>().indicePos == indiceObjeto ||
                       (other.gameObject.GetComponent<slotsBateaValidosExample>().validoSec && other.gameObject.GetComponent<slotsBateaValidosExample>().segundoIndice == indiceObjeto))
                    {
                        //Debug.Log("Coincide Indice");
                        objetoCorrecto = true;
                    }
                    else
                    {
                        //Debug.Log("no coincide indice");
                        objetoCorrecto = false;
                    }
                }
            }
            else 
            {
                //Debug.Log("Slot esta ocupado");
                other.gameObject.GetComponent<slotsBateaValidosExample>().materialesOff = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<slotsBateaValidosExample>())
        {
            //Debug.Log("objeto fuera de Slots Batea");
            objetoCorrecto = false;
            dentroDeTrigger = false;
        }
        if (other.gameObject.CompareTag("InstrumentosTrigger"))
        {
            slotOcupado = false;
        }
    }

    public void colocarObjetoSlot()
    {
        if (dentroDeTrigger && !slotOcupado)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetPositionAndRotation(tranformSlot.position, tranformSlot.rotation);
        }
        else
        {
            parentInteractable();
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    void parentInteractable()
    {
        if (!dentroDeTrigger)
        {
            transform.SetParent(interactableGroup);
        }
    }
    public void mostrarPosicionObjeto()
    {
        if (checkInstrumentosNecesariosExample.Instance.comprobarPosicionesObjetos)
        {
            flechaPosicionInstrumento.SetActive(true);
        }
    }
    public void ocultarPosicionObjeto()
    {
        flechaPosicionInstrumento.SetActive(false);
    }
}

