using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // Velocidad del proyectil
    [SerializeField]private float maxDistance; // Distancia máxima a la que el proyectil puede llegar
    private Vector3 startPosition; // Posición inicial del proyectil
    private bool returning = false; // Indica si el proyectil está regresando al jugador
    public Transform playerTransform; // Referencia al transform del jugador

    private Vector3 direction; // Dirección en la que se dispara el proyectil

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
    
        if (!returning)
        {
            // Mover el proyectil en la dirección del disparo
            transform.position += direction * bulletSpeed * Time.deltaTime;

            // Calcular la distancia recorrida por el proyectil
            float distance = Vector3.Distance(startPosition, transform.position);
         
            // Si el proyectil ha alcanzado la distancia máxima, iniciar el retorno
            if (distance >= maxDistance)
            {
                returning = true;
            }
        }
        else
        {
            // Mover el proyectil hacia la posición inicial del jugador a velocidad normal
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, bulletSpeed );

            // Si el proyectil ha regresado a la posición inicial, destruirlo
            if (Vector3.Distance(transform.position, playerTransform.position) < 0.05f)
            {
                Debug.Log("A");
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        transform.right = direction;
    }

    public void SetMaxDistance(float distance)
    {
        maxDistance = distance;
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemig")) 
            {
            other.GetComponent<ChargingEnemy>().Freze();
            }
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.hasAmmo = true;
                Debug.Log("Bullet returned, ammo available again");
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            // Si el proyectil colisiona con un objeto en el layer "Neutral", regresa al jugador a velocidad normal
            returning = true;
        }
    }
}
