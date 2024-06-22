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
                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = Vector3.down;
                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", -1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDirection = Vector3.left;
                animator.SetFloat("moveX", -1);
                animator.SetFloat("moveY", 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = Vector3.right;
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
            }

            // Si se ha detectado una dirección de movimiento, comenzar el movimiento
            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("isMoving", true);
                StartCoroutine(MoveStep());
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private IEnumerator MoveStep()
    {
        isMoving = true;

        // Calcular la posición objetivo
        Vector3 targetPosition = transform.position + moveDirection * moveDistance;

        // Mover hacia la posición objetivo
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
            yield return null;
        }

        // Asegurarse de que la posición es exacta
        transform.position = targetPosition;

        // Pausar antes del siguiente paso
        yield return new WaitForSeconds(pauseTime);

        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NonTraversable>() != null)
        {
            // Implementar la lógica para manejar la colisión con un objeto no atravesable
            Debug.Log("Colisión con un objeto no atravesable");
        }
    }
}
