using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultaScript : MonoBehaviour
{
	//Declaro la variable oculta para capturar el objeto a acoultar/mostrar
	public GameObject oculta;
	void Start () {
		//Oculto la plataforma al arrancar el juego
		oculta.SetActive(false);
	}

}
