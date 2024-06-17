using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrutoTemporal : MonoBehaviour

{
    public float speed = 5f;
    public float returnSpeed = 7f;
    private Vector3 target;
    private bool isReturning = false;
    private Transform player;

    void Update()
    {
        if (isReturning)
        {
            // Movimiento de regreso al jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, returnSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Movimiento inicial
            transform.position += target * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 direction, Transform playerTransform)
    {
        target = direction.normalized;
        player = playerTransform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReturning && !other.CompareTag("Player"))
        {
            // Si colisiona con algo que no es el jugador, comienza a regresar
            isReturning = true;
        }
    }
}
