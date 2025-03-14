using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
public Transform player; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 2, -4); // Ajusta según sea necesario

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset; // Sigue al jugador
            transform.rotation = Quaternion.Euler(10, 0, 0); // Mantiene una rotación fija
        }
    }
}
