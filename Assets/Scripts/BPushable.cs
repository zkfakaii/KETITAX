using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPushable : MonoBehaviour
{
    public float pushSpeed = 2.0f; // Velocidad de empuje
    public float pushDistance = 1.0f; // Distancia a empujar en cada paso
    public LayerMask collisionLayer; // Capa de colisión para detectar otros objetos empujables y paredes
    public LayerMask blockingLayer; // Capa de bloqueo para detectar objetos que bloquean el empuje

    private bool isPushed = false; // ¿Está el objeto siendo empujado?
    private Rigidbody2D rb;
    private Vector2 pushDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isPushed)
        {
            DetectPush();
        }
    }

    void DetectPush()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDirection, 1f);
   
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("suicido)");
            Vector2 direction = hit.collider.GetComponent<PlayerMovement>().moveDirection;
            Debug.Log("yoanmatate");

            if (direction != Vector2.zero)
            {
                // Verificar si hay colisión en la dirección de empuje
                bool willCollide = Physics2D.RaycastAll(transform.position, direction, 1f, blockingLayer).Length > 0;

                if (!willCollide)
                {
                    pushDirection = direction;
                    StartCoroutine(PushObject());
                }
                else
                {
                    Debug.Log("Empuje bloqueado en la dirección: " + direction);
                    // Bloquear el movimiento del jugador
                    hit.collider.GetComponent<PlayerMovement>().isBlocked = true;
                }
            }
        }
    }

    private IEnumerator PushObject()
    {
        isPushed = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (Vector3)pushDirection * pushDistance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, pushSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }

        rb.MovePosition(targetPosition);

        // Desbloquear el movimiento del jugador después de empujar
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().isBlocked = false;
        }

        isPushed = false;
    }
    

   
}
