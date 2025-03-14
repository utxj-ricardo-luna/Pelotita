using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float tiempo = 0; //Para contabilizar el tiempo
    public bool isJuego = true; //Para saber si estoy jugando y que se incremente el tiempo cuando entre en la escena de Juego
    public int vidas = 3; //para contabilizar las vidas
    public int monedas = 20; //para contabilizar las monedas restantes
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {      
    }
}
