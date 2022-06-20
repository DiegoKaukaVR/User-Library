using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_StepController : MonoBehaviour
{
    [SerializeField]
    private List<scr_Step> steps;

    public int stepIndex = 0;
    
    [Header("Mostrar segundos primer panel")]
    public TextMeshProUGUI[] textContador;
    float tiempo;
    float tiempoMostrar;

    [Header("Dividir steps activos")]
    public GameObject[] stepsBloque_1;
    public GameObject[] stepsBloque_2;
    public GameObject[] stepsBloque_3;

    private void Awake()
    {
        steps = new List<scr_Step>();

        foreach (var item in GetComponentsInChildren<scr_Step>())
        {
            steps.Add(item);
        }
    }
    private void Start()
    {
        InitializeSteps();
        StartCoroutine(delayDesSteps());
    }
    private void Update()
    {
            // para cambiar el texto a "Evaluacion comienza en:"
        if (DatosImportantes.comenzarEvaluacion)
        {
            for (int i = 0; i < textContador.Length; i++)
            {
                textContador[i].text = "Evaluación comienza en : " + tiempoMostrar.ToString() + " seg";
            }
        }
        else
        {
            for (int i = 0; i < textContador.Length; i++)
            {
                textContador[i].text = "Continua en : " + tiempoMostrar.ToString() + " seg";
            }
        }
    }

    public void InitializeSteps()
    {
        steps[stepIndex].StartStep();
    }

    public void NextStep()
    {
        //if (DatosImportantes.modoForma)
        //{
            steps[stepIndex].EndStep();
            StartCoroutine(TempCambioStep());
        //}

        //stepIndex++;

        //steps[stepIndex].StartStep();
    }
    public void NextStepTiempo(float segundos)
    {
        tiempo = segundos;
        StartCoroutine(tiempoContador());
        StartCoroutine(tiempoNextStep(tiempo));
    }

    // para no superponer paneles
    private IEnumerator TempCambioStep()
    {
        //steps[stepIndex].EndStep();

        yield return new WaitForSecondsRealtime(1.0f);
        stepIndex++;

        steps[stepIndex].StartStep();
    }
    IEnumerator tiempoContador()
    {
        while (tiempo >= 0)
        {
            tiempo -= Time.deltaTime;
            tiempoMostrar = tiempo;
            tiempoMostrar = Mathf.RoundToInt(tiempoMostrar);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator tiempoNextStep(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        steps[stepIndex].EndStep();
        yield return new WaitForSeconds(0.2f);
        stepIndex++;
        steps[stepIndex].StartStep();
        if (DatosImportantes.comenzarEvaluacion)
        {
            DatosImportantes.comenzarEvaluacion = false;
        }
    }

    #region activarStepsABloques
    public void desactivarStepsPrimeros()
    {
        foreach (var item in stepsBloque_1)
        {
            item.SetActive(false);
        }
    }
    public void activarStepsSegundos()
    {
        foreach (var item in stepsBloque_2)
        {
            item.SetActive(true);
        }
    }
    public void desactivarStepsSegundos()
    {
        foreach (var item in stepsBloque_2)
        {
            item.SetActive(false);
        }
    }
    public void activarStepsTerceros()
    {
        foreach (var item in stepsBloque_3)
        {
            item.SetActive(true);
        }
    }
    public void desactivarStepsTerceros()
    {
        foreach (var item in stepsBloque_3)
        {
            item.SetActive(false);
        }
    }
    IEnumerator delayDesSteps()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (var item in stepsBloque_2)
        {
            item.SetActive(false);
        }
        foreach (var item in stepsBloque_3)
        {
            item.SetActive(false);
        }
    }
    #endregion

}