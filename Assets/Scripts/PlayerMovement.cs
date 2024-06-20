using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // velocidad de movimiento
    public float moveDistance = 0.5f; // distancia a moverse en cada paso
    public float pauseTime = 0.1f; // tiempo de pausa entre pasos

    private Vector3 moveDirection; // dirección de movimiento
    private bool isMoving = false; // ¿está el personaje moviéndose actualmente?
    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // obtener la entrada del jugador
        moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        // Ajustar la rotación del personaje
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        }
    }

    void FixedUpdate()
    {
        // Mover al personaje
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
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
