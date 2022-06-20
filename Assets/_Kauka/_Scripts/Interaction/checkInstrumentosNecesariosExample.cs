using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class checkInstrumentosNecesariosExample : Singleton<checkInstrumentosNecesariosExample>
{
    // objetosNecesarios la lista de objetos necesarios que estan puestas en el inspector
    public List<primitives> objetosNecesarios;

    public List<primitives> instActual;

    [SerializeField]
    TextMeshProUGUI textoNecesarios, textoNoNecesarios;
    int indexNecesarios, indexNoNecesarios = 0;

    [SerializeField]
    GameObject backgroundPanel;
    [SerializeField]
    GameObject textosNecesariosNo;
    [SerializeField]
    GameObject textoRevisarPosiciones;

    int ah = 0; // para que sea true, si hubiera mas casos numerar / nombrar
    // booleanas para dar por bueno cada instrumento necesario y bien colocado
    public bool[] instrumentacionCorrecta = new bool[5];
    public bool[] instrumentacionCorrectaColocada = new bool[5];

    bool Necesarios = false;
    
    [Tooltip("Esto se activa al comprobar si los objetos estan correctamente colocadas en el slot, si esta mal, al coger un objeto se activaran las flechas mostrando la posicion correcta")]
    // boolean true al pulsar sobre comprobar en la experiencia y no ser correcta las posiciones de los objetos
    public bool comprobarPosicionesObjetos;

    [Space(10)]
    public GameObject botonAutocompletar;

    private void Start()
    {
        textoNecesarios.text = "";
        textoNoNecesarios.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<tipoDeObjetoExample>())
        {
            if (!other.GetComponent<tipoDeObjetoExample>().objetoAñadidoEnLista)
            {
                instActual.Add(other.GetComponent<tipoDeObjetoExample>().primitives);
                //Debug.Log("Añado el elemento: " + other.GetComponent<tipoDeObjeto>().instrumentacionName.ToString());
                other.gameObject.GetComponent<tipoDeObjetoExample>().objetoAñadidoEnLista = true;
            }
            //hacer comparativa bien (Aqui se diferenciaria los casos y que objetos recoger)
            if (ah == 0)
            {
                if (other.gameObject.GetComponent<tipoDeObjetoExample>().primitives == primitives.Cube)
                {
                    instrumentacionCorrecta[0] = true;
                    instrumentacionCorrectaColocada[0] = other.gameObject.GetComponent<tipoDeObjetoExample>().objetoCorrecto;
                }
                if (other.gameObject.GetComponent<tipoDeObjetoExample>().primitives == primitives.Capsule)
                {
                    instrumentacionCorrecta[1] = true;
                    instrumentacionCorrectaColocada[1] = other.gameObject.GetComponent<tipoDeObjetoExample>().objetoCorrecto;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<tipoDeObjetoExample>())
        {
            #region objetosEspecificadosAlQuitarElementoLista
            // esto esta por si peta el que no esta espeficado al salir del exit

            //if (other.gameObject.GetComponent<tipoDeObjeto>().instrumentacionName == instrumentacionHospital.Cube)
            //{
            //    //Debug.Log("sale el cubo");
            //    instrumentacionCorrecta[0] = other.gameObject.GetComponent<tipoDeObjeto>().objetoCorrecto;
            //    other.gameObject.GetComponent<tipoDeObjeto>().objetoAñadidoEnLista = false;
            //    // señalar que tiene que sacar el instrumento del area de la mesa a preparar
            //    QuitarElemento(other.GetComponent<tipoDeObjeto>().instrumentacionName);
            //}
            //if (other.gameObject.GetComponent<tipoDeObjeto>().instrumentacionName == instrumentacionHospital.Capsule)
            //{
            //    //Debug.Log("sale la capsula");
            //    instrumentacionCorrecta[1] = other.gameObject.GetComponent<tipoDeObjeto>().objetoCorrecto;
            //    other.gameObject.GetComponent<tipoDeObjeto>().objetoAñadidoEnLista = false;
            //    // señalar que tiene que sacar el instrumento del area de la mesa a preparar
            //    QuitarElemento(other.GetComponent<tipoDeObjeto>().instrumentacionName);
            //}
            #endregion objetosEspecificadosAlQuitarElementoLista
            #region objetosNoEspecificadosAlQuitarElementoLista
            for (int i = 0; i < instrumentacionCorrecta.Length; i++)
            {
                instrumentacionCorrecta[i] = false;
                instrumentacionCorrectaColocada[i] = other.gameObject.GetComponent<tipoDeObjetoExample>().objetoCorrecto;
            }
                other.gameObject.GetComponent<tipoDeObjetoExample>().objetoAñadidoEnLista = false;
                // señalar que tiene que sacar el instrumento del area de la mesa a preparar
                QuitarElemento(other.GetComponent<tipoDeObjetoExample>().primitives);
            #endregion objetosNoEspecificadosAlQuitarElementoLista
        }
    }

    //llamar al llevar batea
    public void checkIfObjetosNecesarios()
    {
        //hacer comparativa bien (Aqui se diferenciaria los casos ->
        if (ah == 0)
        {
            ActualizarTextosInstrumentacion();

            // <-  y comprobar si tiene los objetos necesarios)
            if (instrumentacionCorrecta[0] && instrumentacionCorrecta[1] && Necesarios)
            {
                Debug.Log("Instrumentos Correctos/Necesarios");
                // se desactivaria el panel con la lista de los objetos necesarios
                textosNecesariosNo.SetActive(false);
                // se desactivarian los instrumentos restantes para no poder cogerlos una vez la mesa este correcta
                
                // Comprobar la posicion de los objetos colocados
                if(instrumentacionCorrectaColocada[0] && instrumentacionCorrectaColocada[1])
                {
                    Debug.Log("instrumentacion correctamente colocada");
                    // desactivar panel lista entero - flechas - y no poder volver a tocar los instrumentos
                    backgroundPanel.SetActive(false);
                    textoRevisarPosiciones.SetActive(false);
                    // desactivar boton autocompletar
                    botonAutocompletar.SetActive(false);
                }
                else
                {
                    Debug.Log("Instrumentacion mal colocada");
                    // activador de flechas para mostrar posicion del objeto seleccionado
                    comprobarPosicionesObjetos = true;
                    // activar texto "Revisar Posicion de Objetos"
                    backgroundPanel.SetActive(true);
                    textoRevisarPosiciones.SetActive(true);
                    botonAutocompletar.SetActive(true);
                }
                //listaObjetosMal.SetActive(false);
            }
            else
            {
                Debug.Log("Necesitas o sobran instrumentos");
                backgroundPanel.SetActive(true);
                textosNecesariosNo.SetActive(true);
                botonAutocompletar.SetActive(true);
                ActualizarTextosInstrumentacion();
            }
        }
    }

    // llamar al sacar el objeto de la batea
    public void QuitarElemento(primitives inst)
    {
        //Debug.Log("quito el elemento: " + inst.ToString());

        if (instActual.Contains(inst))
        {
            //Debug.Log("quito el elemento porque esta en la lista: " + inst.ToString());

            instActual.Remove(inst);
        }
    }

    public void ActualizarTextosInstrumentacion()
    {
        textoNoNecesarios.text = "";
        textoNecesarios.text = "";
        indexNecesarios = 0;
        indexNoNecesarios = 0;

        List<primitives> temp = new List<primitives>();

        foreach (var item in instActual)
        {
            // por cada objeto añadido a instActual (objetos triggereados) añadirlo a la lista temporal
            temp.Add(item);
        }
        //temp = instActual;

        if (ah == 0)
        {
            // objetosNecesarios es la lista de objetos necesarios que estan puestas en el inspector
            foreach (var item in objetosNecesarios)
            {
                // ------------ COMPRUEBA LOS OBJETOS QUE NECESITAS ---------------

                // si la lista temporal no contiene los objetos de la lista de objetosNecesarios ....
                if (!temp.Contains(item))
                {
                    // .... y es menor a 9, los escribe en el panel de unity ....
                    if(indexNecesarios < 9)
                    {
                                      // despues de .ToString() poner .Replace("1","2") para sustituir el valor de 1 por 2, osea cambiar "_" por " " por los enums que no pillan espacios
                        textoNecesarios.text += item.ToString() + "\n";
                        indexNecesarios++;
                    }
                    // .... si es mayor a 9, añade puntos
                    else if(indexNecesarios == 9)
                    {
                        textoNecesarios.text += "...";
                        indexNecesarios++;
                    }
                }
                // si la lista temporal contiene los objetos de la lista de objetosNecesarios ....
                else
                {
                    // .... quita el objeto de la lista temporal
                    // para que no lo escriba despues en el panel de unity
                    // y al quitar el objeto de la lista temporal no apareceria como objeto no necesario despues
                    temp.Remove(item);
                }
            }

            // ------------ COMPRUEBA LOS OBJETOS QUE NO NECESITAS ---------------
            // por cada objeto de la lista temporal....
            foreach (var item in temp)
            {
                // .... escribe en el panel de unity todos los objetos que contenga la lista temporal y
                // al haber quitado todos los objetos que coincide con la lista de objetosNecesarios (colocados en la mesa/batea (trigger))
                // se entiende que el resto de objetos no son necesarios
                if (indexNoNecesarios < 9)
                {
                                    // despues de .ToString() poner .Replace("1","2") para sustituir el valor de 1 por 2, osea cambiar "_" por " " por los enums que no pillan espacios
                    textoNoNecesarios.text += item.ToString() + "\n";
                    indexNoNecesarios++;
                }
                else if (indexNoNecesarios == 9)
                {
                    textoNoNecesarios.text += "...";
                    indexNoNecesarios++;
                }
            }

            if(indexNoNecesarios >= 1)
            {
                Necesarios = false;
            }
            else
            {
                Necesarios = true;
            }
        }
    }
}
