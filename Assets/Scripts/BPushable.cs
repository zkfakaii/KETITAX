using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPushable : MonoBehaviour
{
    public enum PushableState { Normal, Stopped }
    public PushableState currentState = PushableState.Normal; // Estado actual del objeto

    public float pushSpeed = 2.0f; // Velocidad de empuje
    public float pushDistance = 1.0f; // Distancia a empujar en cada paso
    public LayerMask collisionLayer; // Capa de colisión para detectar otros objetos empujables y paredes
    public LayerMask blockingLayer; // Capa de bloqueo para detectar objetos que bloquean el empuje

    public Sprite normalSprite; // Sprite para el estado Normal

    private bool isPushed = false; // ¿Está el objeto siendo empujado?
    private Rigidbody2D rb;
    private Vector2 pushDirection;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLayerAndSprite();
    }

    void Update()
    {
        if (!isPushed && currentState == PushableState.Normal)
        {
            DetectPush();
        }
    }

    void DetectPush()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDirection, 1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Vector2 direction = hit.collider.GetComponent<PlayerMovement>().moveDirection;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && currentState == PushableState.Stopped)
        {
            currentState = PushableState.Normal;
            UpdateLayerAndSprite();
            Destroy(other.gameObject); // Destruye el bullet
        }
    }

    private void UpdateLayerAndSprite()
    {
        gameObject.layer = currentState == PushableState.Stopped ? LayerMask.NameToLayer("Walls") : LayerMask.NameToLayer("Default");
        if (currentState == PushableState.Normal && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    void OnValidate()
    {
        // Asegúrate de que el estado y el sprite se actualicen en el editor cuando se modifique
        if (!Application.isPlaying)
        {
            UpdateLayerAndSprite();
        }
    }
}
