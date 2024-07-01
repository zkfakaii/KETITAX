using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento
    public float moveDistance = 0.5f; // Distancia a moverse en cada paso
    public float pauseTime = 0.1f; // Tiempo de pausa entre pasos

    public Vector3 moveDirection; // Dirección de movimiento
    private bool isMoving = false; // ¿Está el personaje moviéndose actualmente?
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] LayerMask collisionLayer;
    bool willCollide = false;
    private PlayerInventory playerInventory;

    public bool isBlocked = false; // ¿Está el movimiento bloqueado?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // Solo aceptar input si no está moviéndose y no está bloqueado
        if (!isMoving && !isBlocked)
        {
            moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.up;

                animator.SetFloat("moveY", 1);
                animator.SetFloat("lastMoveY", 1);
                playerInventory.SetShootDirection(Vector3.up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = Vector3.down;

                animator.SetFloat("moveY", -1);
                animator.SetFloat("lastMoveY", -1);
                playerInventory.SetShootDirection(Vector3.down);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDirection = Vector3.left;

                animator.SetFloat("moveX", -1);
                animator.SetFloat("lastMoveX", -1);
                playerInventory.SetShootDirection(Vector3.left);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = Vector3.right;
                animator.SetFloat("moveX", 1);
                animator.SetFloat("lastMoveX", 1);
                playerInventory.SetShootDirection(Vector3.right);

            }

            if (moveDirection != Vector3.left && moveDirection != Vector3.right)
            {
                animator.SetFloat("moveX", 0);
            }

            willCollide = Physics2D.RaycastAll(this.transform.position, moveDirection, 1, collisionLayer).Length > 0;

            // Si hay movimiento, iniciar la corrutina para el paso
            if (moveDirection != Vector3.zero && !willCollide)
            {
                StartCoroutine(MoveStep());
            }
        }
    }

    private IEnumerator MoveStep()
    {
        Debug.Log("MoveStep coroutine started");
        animator.SetBool("isMoving", true);
        isMoving = true;

        // Calcular la posición objetivo
        Vector3 targetPosition = transform.position + moveDirection * moveDistance;
        Debug.Log("Target Position: " + targetPosition);

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
        animator.SetBool("isMoving", false);

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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);
    }

    private void OnDrawGizmos()
    {
        if (willCollide)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + moveDirection);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(this.transform.position, this.transform.position + moveDirection);
        }
    }
}
