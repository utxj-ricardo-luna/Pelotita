using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;
    //Para registrar la diferencia entre la posición de la cámara y la del jugador
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
    //Referencia a nuestro jugador
    // Se ejecuta cada frame, pero después de haber procesado todo. Es más exacto para la cámara
    void LateUpdate()
    {
        //Actualizo la posición de la cámara
        transform.position = jugador.transform.position + offset;
    }
}
