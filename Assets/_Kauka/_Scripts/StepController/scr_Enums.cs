using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.ComponentModel.DataAnnotations;



// Clase para recopilar todos los enums

public class scrEnums : MonoBehaviour {
    
}

public enum enm_PlayerPrefs
{
    CurrentLanguage
}

/// <summary>
/// Lista de idiomas en los que se podra cambiar la aplicacion
/// </summary>
public enum Idioma
{
    ESP,
    ENG,
    EUS
}

/// <summary>
/// Lista de modos en los que ejecutar la aplicacion, este cargara el paso deseado al inicio
/// </summary>
public enum GameMode
{
    Tutorial = 1,
    Application,
    End,   
    Other
}

/// <summary>
/// Lista de pasos que tendran que realizar en la aplicacion
/// </summary>
public enum Pasos
{
   Bienvenida = 1,  
   FIN
}

/// <summary>
/// MAndo izquierdo o derecho
/// </summary>
public enum MandoLaser
{
    izquierda = 0,
    Derecha
}

/// <summary>
/// Tipo de panel para el control al inicio de este. Activa iconos e imagenes automaticamente
/// </summary>
public enum panelTypes
{
    panel_icon = 0,
    panel_graphic,
    panel_empty
}


public enum instrumentacion
{
    Bisturi = 0,
    AmpollaSueroFisiologico,
    SulfaniazinaArgentica,
    AcidosGrasosHiperoxigenados,
    Alginato,
    ApositoDeHidrofibraDeHidrocoloide,
    ApositoDePlata,
    AskinaSorbAlginato,
    Hisopo,
    IntransiteGel,
    PomadaAnestesicaLocal,
    AllevynLife,
    Gasas,
    mepilex,
    pastaLassar,
    Hidrogel,
    AllevynHeel,
    SprayDeBarreraProtectorCutanea,
    Empapador,
    VendaTubular,
    Aquacel,
    PinzasKocher,
    JeringaSuero,
    TorundaConGasa,
    FilmTransparente,
    PinzasAdsonSinDientes,
    Clorhexidina,
    Mascarilla,
    AllevynSacrum,
    GuantesBlancos,
    ContenedorDePunzantes
}

public enum primitives
{
    Capsule = 0,
    Cylinder,
    Sphere,
    Cube,
    Plane,
    Quad
}

public enum instHospitalDonosti
{
    Aspirador = 0,
    Aspirador_recto,
    Batea,
    Bisturi,
    Crawford,
    Disector_recto,
    Disector_curvo,
    Disector_negro,
    Farabeuf,
    Pinza_mosquito,
    Pinza_diseccion_con_dientes,
    Pinza_de_mayo_curva,
    Plastico_alexisXS,
    Tijera_de_mayo_recta,
    Trocar_12mm,
    Disector_D8amico, //8 cambiar por "´"
    Disector_snake,
    Pinza_de_agarre,
    Bolita,
    Harmonico,
    Echelon_35,
    Echelon_60,
    Hemolock,
    Bolsa,
    Bote_pequeño,
    Suero,
    Bote_grande
}

public enum cargasEchelon
{
    CargaVascular = 0,
    CargaVerde,
    CargaAzul,
    Clip,
    ClipTrigger
}

#region SAVI Classes

/// <summary>
/// Clase encargada de la creacion del JSON para el envio al backend
/// </summary>
public class InfoSession
{
    public int UserId;
    public int SessionId;
    public int Duration;
    private string token;

    public List<InfoTarea> Tasks;

    public int GetID { get => SessionId; }
    public string GetToken { get => token; }
    public int GetPlayer { get => UserId; }

    public InfoSession(string T, int id, int userID)
    {
        UserId = userID;
        token = T;
        SessionId = id;
    }
}

/// <summary>
/// Clase encargada de la creacion de las tareas de forma apta para añadir al json
/// </summary>
[Serializable]
public class InfoTarea
{
    public string task_title;
    public int taskID;

    public List<InfoVariable> variableList;

    public InfoTarea(string T, int Id, Dictionary<Variable, int> TempVar)
    {
        task_title = T;
        taskID = Id;

        variableList = new List<InfoVariable>();

        foreach (var variable in TempVar)
        {
            variableList.Add(new InfoVariable(variable.Key, variable.Value));
        }
    }
}

/// <summary>
/// Clase encargada de la creacion de la lista de variables de cada tarea aptas para el JSON
/// </summary>
[Serializable]
public class InfoVariable
{
    public string name;
    public int id_value;
    public int value;

    public InfoVariable(Variable var, int v)
    {
        name = var.name;
        id_value = var.id_value;
        value = v;
    }
}

public class infoVueltaCerrarSesion
{
    public string ack;

    public string code;

    public string msg;
}

public class InfoVueltaInicioSesion
{
    public string sessionId;

    public string msg;
}

public class InfoVueltaListaExperiencias
{
    public ExperienceList[] experienceList;
}

[Serializable]
public class ExperienceList
{
    public int id;
}

/// <summary>
/// La clase tiempo es la encargada de registrar los tiempos de cada tarea y hacer su tratamiento
/// </summary>
[Serializable]
public class Tiempo
{
    private DateTime Inicio;
    private DateTime Final;
    public int TiempoTotal;

    public Tiempo()
    {
        Inicio = DateTime.Now;
    }

    public int CalcularTiempo()
    {
        Final = DateTime.Now;

        TiempoTotal += (int)Final.Subtract(Inicio).TotalSeconds;

        return TiempoTotal;
    }

    public void ReiniciarContador()
    {
        Inicio = DateTime.Now;
    }
}

[Serializable]
public class Tarea
{
    private string Task_Name;
    private int Task_ID;

    public bool Mandar = false;

    public Dictionary<string, int> Variables;

    public string Nombre1 { get => Task_Name; set => Task_Name = value; }
    public int ID_Tarea1 { get => Task_ID; set => Task_ID = value; }

    public Tarea(string name, int ID, Dictionary<string, int> vs)
    {
        Task_Name = name;

        Task_ID = ID;

        Variables = new Dictionary<string, int>();

        Variables = vs;
    }
}

/// <summary>
/// La clase variable se utiliza para encapsular el nombre y el ID de una variable y poder agregarla al diccionario
/// </summary>
[Serializable]
public class Variable
{
    public string name;
    public int id_value;

    public Variable(string n, int i)
    {
        name = n;
        id_value = i;
    }
}
/*
[Serializable]
public class Hito
{
    private string Nombre = "Estándar de Proceso";
    private int ID_Tarea = 0;

    public bool Mandar = true;

    public Dictionary<Variable, int> Variables;

    public string GetName { get => Nombre; }
    public int GetID { get => ID_Tarea; }

    public Hito(List<Variable> vars, int ID, string nombre)
    {
        Variables = new Dictionary<Variable, int>();
        Variables.Add(vars[ID], 0);

        ID_Tarea = ID;
        Nombre = nombre;
    }

    public void SetValue(int value)
    {
        Variables[MilestoneManager.listaVariables[ID_Tarea]] = value;
    }
}

public class Medicacion
{
    private string Nombre = "Medicacion";
    private int ID_Tarea = 0;

    public bool Mandar = false;

    public Dictionary<Variable, int> Variables;

    public string GetName { get => Nombre; }
    public int GetID { get => ID_Tarea; }

    public Medicacion(List<Variable> vars)
    {
        Variables = new Dictionary<Variable, int>();

        Variables.Add(vars[0], 0);
        Variables.Add(vars[1], 0);
        Variables.Add(vars[2], 0);
    }

    public void RellenarCampo(int campo, int value)
    {
        for (int i = 0; i < Variables.Count; i++)
        {
            Variables[MilestoneManager.listaVariables[campo]] = value;
        }
    }
}

public class TareaInicio
{
    private string Nombre = "Tarea_Inicio";
    private int ID_Tarea = 0;

    public bool Mandar = false;

    public Dictionary<Variable, int> Variables;

    public string GetName { get => Nombre; }
    public int GetID { get => ID_Tarea; }

    public void SetID(int _id)
    {
        ID_Tarea = _id;
    }

    public TareaInicio(List<Variable> vars)
    {
        Variables = new Dictionary<Variable, int>();

        Variables.Add(vars[0], 0);       
    }

    public void RellenarCampo(int campo, int value)
    {
        for (int i = 0; i < Variables.Count; i++)
        {
            Variables[MilestoneManager.listaVariables[campo]] = value;
        }
    }
}
*/

#endregion SAVI Classes
