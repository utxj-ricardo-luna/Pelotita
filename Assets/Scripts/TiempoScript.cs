using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiempoScript : MonoBehaviour
{
    //Variable para asociar el objeto Texto Tiempo
    public Text textoTiempo;
    //Script GameManager
    private GameManager gameManager;
    private PlayerScript jugador;
    void Start()
    {
        //Inicializo el texto del contador de tiempo
        textoTiempo.text = "00:00";
        //Capturo el script de GameManager
        gameManager = FindObjectOfType<GameManager>();
        jugador = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        if(jugador.login){
            //Escribo tiempo transcurrido (si no se ha acabado el juego)
            if (gameManager.isJuego)
            {
                textoTiempo.text = "" + formatearTiempo();
            }
        }
    }
    //Formatear tiempo (público porque la necesitaremos más adelante)
    public string formatearTiempo()
    {
        //Añado el intervalo transcurrido a la variable tiempo
        if (gameManager.isJuego)
        {
            gameManager.tiempo += Time.deltaTime;
        }
        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(gameManager.tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(gameManager.tiempo % 60).ToString("00");
        //Devuelvo el string formateado con : como separador
        return minutos + ":" + segundos;
    }
}
