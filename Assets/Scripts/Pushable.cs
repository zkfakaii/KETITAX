using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour

{
    public float moveSpeed = 2.0f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb; // Rigidbody del jugador

    private GameObject pushableObject; // Objeto empujable actualmente en contacto

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("PlayerMovement script initialized");
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector2.up;
            Debug.Log("W key pressed: Moving Up");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector2.down;
            Debug.Log("S key pressed: Moving Down");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector2.left;
            Debug.Log("A key pressed: Moving Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector2.right;
            Debug.Log("D key pressed: Moving Right");
        }

        // Si hay un objeto empujable y el jugador intenta moverse en la dirección del objeto, empujarlo
        if (pushableObject != null && moveDirection != Vector2.zero)
        {
            Debug.Log("Attempting to push object: " + pushableObject.name + " in direction: " + moveDirection);
            PushObject(pushableObject, moveDirection);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Empujable"))
        {
            pushableObject = other.gameObject;
            Debug.Log("Entered trigger with pushable object: " + pushableObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == pushableObject)
        {
            Debug.Log("Exited trigger with pushable object: " + pushableObject.name);
            pushableObject = null;
        }
    }

    void PushObject(GameObject obj, Vector2 direction)
    {
        // Asegurarse de que el objeto sea empujable
        if (!obj.CompareTag("Empujable"))
        {
            Debug.LogWarning("Attempted to push a non-pushable object: " + obj.name);
            return;
        }

        // Obtener el Rigidbody2D del objeto empujable
        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

        if (objRb == null)
        {
            Debug.LogWarning("The pushable object " + obj.name + " does not have a Rigidbody2D component");
            return;
        }

        // Calcular la fuerza de empuje (puedes ajustar esta fuerza según sea necesario)
        Vector2 pushForce = direction.normalized * moveSpeed * Time.deltaTime;

        // Aplicar la fuerza al objeto empujable
        objRb.AddForce(pushForce, ForceMode2D.Impulse);
        Debug.Log("Pushing object: " + obj.name + " with force: " + pushForce);
    }
}
