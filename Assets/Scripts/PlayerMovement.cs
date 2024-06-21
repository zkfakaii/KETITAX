using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento
    public float moveDistance = 0.5f; // Distancia a moverse en cada paso
    public float pauseTime = 0.1f; // Tiempo de pausa entre pasos

    private Vector3 moveDirection; // Direcci�n de movimiento
    private bool isMoving = false; // �Est� el personaje movi�ndose actualmente?
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Solo aceptar input si no est� movi�ndose
        if (!isMoving)
        {
            moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveDirection = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = Vector3.right;
            }

            // Ajustar la rotaci�n del personaje
            if (moveDirection != Vector3.zero)
            {
                StartCoroutine(MoveStep());
                transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            }
        }
    }

    private IEnumerator MoveStep()
    {
        isMoving = true;

        // Calcular la posici�n objetivo
        Vector3 targetPosition = transform.position + moveDirection * moveDistance;

        // Mover hacia la posici�n objetivo
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
            yield return null;
        }

        // Asegurarse de que la posici�n es exacta
        transform.position = targetPosition;

        // Pausar antes del siguiente paso
        yield return new WaitForSeconds(pauseTime);

        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<NonTraversable>() != null)
        {
            // Implementar la l�gica para manejar la colisi�n con un objeto no atravesable
            Debug.Log("Colisi�n con un objeto no atravesable");
        }
    }
}
