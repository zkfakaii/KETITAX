using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb; // Rigidbody del jugador

    private GameObject pushableObject; // Objeto empujable actualmente en contacto

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }

        // Si hay un objeto empujable y el jugador intenta moverse en la dirección del objeto, empujarlo
        if (pushableObject != null && moveDirection != Vector2.zero)
        {
            PushObject(pushableObject, moveDirection);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Empujable"))
        {
            pushableObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == pushableObject)
        {
            pushableObject = null;
        }
    }

    void PushObject(GameObject obj, Vector2 direction)
    {
        // Asegurarse de que el objeto sea empujable
        if (!obj.CompareTag("Empujable"))
        {
            return;
        }

        // Obtener el Rigidbody2D del objeto empujable
        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

        // Calcular la fuerza de empuje (puedes ajustar esta fuerza según sea necesario)
        Vector2 pushForce = direction.normalized * moveSpeed * Time.deltaTime;

        // Aplicar la fuerza al objeto empujable
        objRb.AddForce(pushForce, ForceMode2D.Impulse);
    }
}
