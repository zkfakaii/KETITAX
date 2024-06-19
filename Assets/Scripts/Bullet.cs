using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // Velocidad del proyectil

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aquí puedes añadir lógica para la colisión del proyectil con otros objetos
        Debug.Log("Bullet collided with " + other.name);
        Destroy(gameObject);
    }
}
