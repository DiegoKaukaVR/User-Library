using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DatosImportantes 
{
    //public static int casoUlcera;
    public static GameMode modoJuego;
    public static bool reiniciar;
    // formacion o evaluacion
    public static bool modoForma;
    //Experiencias
    public static bool modoInstrumentos;
    public static bool modoCirujano;
    public static bool modoLibre;


    //SAVI
    public static bool logedIn = false;

    //------------------------------------
    // Trigger mano en los slots de la batea
    public static bool rightHandIsTriggering = false;
    public static bool leftHandIsTriggering = false;
    // Boolean autocompletar que tenga en cuenta que la mesa esta completa y lista (faltaria resetearla)
    public static bool mesaListaAutocompletar;
    // Para iniciar con texto "Comienza evaluacion en:" (o algo así) -> Scripts - StepCtrl y ControlPanelYModoFE
    public static bool comenzarEvaluacion;
    // bool para mostrar flechas de objetos y su posicion, tambien los que no son correctos
    public static bool mostrarObjetosCorrectos;
}