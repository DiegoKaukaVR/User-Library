using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipoDeInstrumento : MonoBehaviour
{
    [Header("StepController")]
    public scr_StepController stepCtrl;
    int stepInt = 0;
    bool cogerStep;

    [Header("Tipo y comprobacion")]
    [Space(10)]
    public instHospitalDonosti instHospitalDonosti;

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
    [Space(10)]
    [Tooltip("Este bool si esta colocado en la misma posicion que el pivot del slot")]
    public bool enSlot;
    // Opcion correcta (no puede ser estatica, a menos que se haga una bool para cada instrumento)
    public bool objetoCorrecto;
    public GameObject flechaPosicionInstrumento;

    [Header("Objeto en mano")]
    public bool objetoEnMano;

    // --- activado cuando se coloca el objeto en un slot (solo modo evaluativo) ---
    GameObject botonComprobar;

    //---- int para ocuparSlot y no aparecer guia -> controlMantenerSlots
    [HideInInspector]
    public int slotCont;
    [Header("Canvas con el x, dentro del objeto")]
    public GameObject xErrorCanvas;

    private void Start()
    {
        if (!DatosImportantes.modoForma && DatosImportantes.modoInstrumentos)
        {
            botonComprobar = GameObject.Find("ButtonComprobar");
            StartCoroutine(desactivarBoton());
        }
    }
    private void Update()
    {
        if (tranformSlot != null)
        {
            if (transform.position == tranformSlot.position)
            {
                enSlot = true;
            }
            else
            {
                enSlot = false;
            }
        }
        else
        {
            enSlot = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("InstrumentosTrigger"))
        {
            slotOcupado = true;
        }
        if (other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>())
        {
            if (!slotOcupado)
            {
                other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().materialesOff = false;

                if (other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().handTriggering)
                {
                    dentroDeTrigger = true;

                    tranformSlot = other.gameObject.transform.parent.transform;
                    //slot correcto con 1 o 2 opciones
                    if (other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().indicePos == indiceObjeto ||
                       (other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().validoSec && other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().segundoIndice == indiceObjeto))
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
                other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>().materialesOff = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<slotsBateaValidosInstrumentacion>())
        {
            tranformSlot = null;
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
        if(tranformSlot != null)
        {
            if (dentroDeTrigger && !slotOcupado)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.SetPositionAndRotation(tranformSlot.position, tranformSlot.rotation);
                #region stepControl
                if (DatosImportantes.modoForma)
                {
                    if (stepInt == 0)
                    {
                        StartCoroutine(nextStepTime());
                    }
                }
                else if(!DatosImportantes.modoForma && DatosImportantes.modoInstrumentos)
                {
                    // al colocar el objeto (un objeto) saltar boton de comprobacion
                    if (!botonComprobar.activeInHierarchy)
                    {
                        botonComprobar.SetActive(true);
                    }
                }
                #endregion
            }
        }
        else
        {
            parentInteractable();
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    IEnumerator nextStepTime()
    {
        yield return new WaitForSeconds(0.1f);
        stepCtrl.NextStep();
        GetComponent<MyGrabInteractable>().enabled = false;
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
    public void nextStepAlCoger()
    {
        if (DatosImportantes.modoForma)
        {
            if (stepInt == 0 && !cogerStep)
            {
                stepCtrl.NextStep();
                cogerStep = true;
                stepInt++;
            }
        }
    }
    public void resetStepInt()
    {
        stepInt = 0;
    }
    public void resetCogerStep()
    {
        stepInt = 0;
        cogerStep = false;
    }

    public void objetoEnManoBool(bool b)
    {
        objetoEnMano = b;
    }
    public void transformSlotNull()
    {
        tranformSlot = null;
        GetComponent<MyGrabInteractable>().enabled = false;
    }
    IEnumerator desactivarBoton()
    {
        yield return new WaitForSeconds(0.02f);
        botonComprobar.SetActive(false);
    }
    public void esteObjetoNoEsCorrecto(bool b)
    {
        if (b)
        {
            xErrorCanvas.SetActive(true);
        }
        else
        {
            xErrorCanvas.SetActive(false);
        }
    }
}

