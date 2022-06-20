using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extenderSobreDosPuntos : MonoBehaviour
{
    public GameObject endPoint;
    public GameObject startPoint;
    public GameObject cilindro;
    Vector3 initialScale;
    public float maxRange = 10f;
    public LayerMask uiMask;
    public GameObject auraImage;
    public float tamCilindro;
    public Material cilMat;
    public Texture valid;
    public Texture invalid;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = cilindro.transform.localScale;
        actualizarEscalaObjeto();
    }

    // Update is called once per frame
    void Update()
    {

        actualizarEscalaObjeto();

        RaycastHit rHit;
        if(Physics.Raycast(transform.position, transform.forward, out rHit, maxRange,uiMask))
        {
            //Debug.Log(rHit.transform.name);
            //Debug.Log("Hit UI");
            endPoint.transform.position = rHit.point;
            endPoint.transform.rotation = Quaternion.FromToRotation(Vector3.forward, rHit.normal);
            auraImage.SetActive(true);
            cilMat.mainTexture = valid;
            
        }
        else
        {
            endPoint.transform.position = transform.position + transform.forward.normalized * maxRange;
            auraImage.SetActive(false);
            cilMat.mainTexture = invalid;
        }
        //Debug.DrawRay(transform.position, transform.forward * maxRange);

    }

    void actualizarEscalaObjeto()
    {
        float distancia = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        cilindro.transform.localScale = new Vector3(initialScale.x, distancia / tamCilindro, initialScale.z);

        Vector3 puntoCentral = (startPoint.transform.position + endPoint.transform.position) / 2;
        cilindro.transform.position = puntoCentral;

        Vector3 direccionRotate = endPoint.transform.position - startPoint.transform.position;
        cilindro.transform.up = direccionRotate;
    }
}
