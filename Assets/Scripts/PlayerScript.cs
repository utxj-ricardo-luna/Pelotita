using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerScript : MonoBehaviourPunCallbacks
{
   // int limiteSuelo = 0;
    //Script GameManager
    public bool login = false;
    private GameManager gameManager;
    private Rigidbody cuerpoPlayer;
    public float velocidad = 5;
    public float salto = 7;
    //Variable para asociar el objeto Mensajes
    public Text textoMensajes;
    public Text textoVidas;
    Vector3 posicionInicial;
    public Text textoMonedas;
    public AudioSource audioMoneda;
    public AudioSource audioSaltar;
    public AudioSource audioPerder;
    public AudioSource audioGameover;
    public AudioSource audioFondo;
    private registerProgres progreso;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        progreso = FindObjectOfType<registerProgres>();
        textoVidas.text = "" + gameManager.vidas;
        textoMensajes.text = "";
        cuerpoPlayer = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        textoMonedas.text = "" + gameManager.monedas;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (login)
        {
        if (photonView.IsMine)
        {
            //Si quedan vidas
            if (gameManager.vidas > 0)
            {
                //Muevo el jugador (si tiene vidas)
                MoverJugador();
                //Si ha caído por debajo del suelo, le quito una vida
                if (transform.position.y < -1)
                {
                    quitarVida();
                }
            }
            //Si no quedan vidas
            else if (gameManager.vidas == 0)
            {
                //audioGameover.Play();
                audioFondo.Stop();
                //Muestro mensaje
                textoMensajes.text = "Juego Terminado";
                //Pongo isJuego a false para que deje de contar el script Tiempo
                gameManager.isJuego = false;

                audioGameover.Play();
            }
            //Si no quedan monedas, gana
            if (gameManager.monedas == 0)
            {
                //Muestro mensaje (si ha completado el juego)
                textoMensajes.text = "¡Has completado el juego!";
                textoMensajes.color = new Color(0, 255, 0); //Color verde
                //Pongo isJuego a false para que deje de contar el script Tiempo
                gameManager.isJuego = false;
            }
        }
        }
    }
    private bool isSuelo()
    {
        //Genero el array de colisiones de la esfera/jugador pasando su centro y su radio
        Collider[] colisiones = Physics.OverlapSphere(transform.position, 0.5f);
        //Recorro ese array y si está colisionando con el suelo devuelvo true
        foreach (Collider colision in colisiones)
        {
            if (colision.tag == "Suelo")
            {
                return true;
            }
        }
        return false;
    }
    void MoverJugador()
    {
        //captura el movimiento en horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        //Generar el vector de movimiento asociado,agregando velocidad
        Vector3 movimiento = new Vector3(movimientoH * velocidad, 0.0f, movimientoV * velocidad);
        //Aplicar el movimiento al rigidbody del jugador
        cuerpoPlayer.AddForce(movimiento);
        //Si pulsa el botón de saltar y está en el suelo
        if (Input.GetButton("Jump") && isSuelo())
        {
            //Aplico el movimiento vertical con la potencia de salto
            cuerpoPlayer.velocity += Vector3.up * salto;
            audioSaltar.Play();
        }
    }
    void quitarVida()
    {
        //Resto una vida
        gameManager.vidas--;
        audioPerder.Play();
        //Actualizo el contador de vidas
        textoVidas.text = "" + gameManager.vidas;
        //Devuelvo el Jugador a su posición inicial y le quito la fuerza
        transform.position = posicionInicial;
        cuerpoPlayer.velocity = Vector3.zero;
    }
    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            audioMoneda.Play();
            //Desactivo el objeto
            other.gameObject.SetActive(false);
            //Decremento el contador de monedas en uno (también se puede hacer como monedas = monedas -1)
            gameManager.monedas--;
            //Actualizo el texto del contador de monedas
            textoMonedas.text = "" + gameManager.monedas;
            StartCoroutine(progreso.RegisterProgresoRequest());
        }
    }
}
