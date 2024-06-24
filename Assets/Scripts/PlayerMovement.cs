using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento
    public float moveDistance = 0.5f; // Distancia a moverse en cada paso
    public float pauseTime = 0.1f; // Tiempo de pausa entre pasos

    private Vector3 moveDirection; // Dirección de movimiento
    private bool isMoving = false; // ¿Está el personaje moviéndose actualmente?
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Solo aceptar input si no está moviéndose
        if (!isMoving)
        {
            moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.up;
                animator.SetTrigger("WalkUp");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = Vector3.down;
                animator.SetTrigger("WalkDown");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDirection = Vector3.left;
                animator.SetTrigger("WalkLeft");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = Vector3.right;
                animator.SetTrigger("WalkRight");
            }

            // Si hay movimiento, iniciar la corrutina para el paso
            if (moveDirection != Vector3.zero)
            {
                StartCoroutine(MoveStep());
            }
        }
    }

    private IEnumerator MoveStep()
    {
        isMoving = true;

        // Calcular la posición objetivo
        Vector3 targetPosition = transform.position + moveDirection * moveDistance;

        // Mover hacia la posición objetivo usando MovePosition
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }

        // Asegurarse de que la posición es exacta
        rb.MovePosition(targetPosition);

        // Pausar antes del siguiente paso
        yield return new WaitForSeconds(pauseTime);

        isMoving = false;
    }

    public IEnumerator MoveAlongRamp(Vector2 direction, float distance)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (Vector3)direction.normalized * distance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }

        rb.MovePosition(targetPosition);

        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<NonTraversable>() != null)
        {
            // Implementar la lógica para manejar la colisión con un objeto no atravesable
            Debug.Log("Colisión con un objeto no atravesable");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);
    }
}
