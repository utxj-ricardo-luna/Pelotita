using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyOculto : MonoBehaviour
{
    public OcultaScript ocultaScript;
   // public GameObject oculta;
    // Start is called before the first frame update
    void Start()
    {
        //ocultaScript = GetComponent<OcultaScript>();
        //ocultaScript.oculta.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Se ejecuta al colisionar con el objeto
  private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //Muestro la plataforma
            ocultaScript.oculta.SetActive(true);
        }
    }
}
