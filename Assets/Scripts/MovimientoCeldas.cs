using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCeldas : MonoBehaviour
{
    public float lerpTime = 0.5f; // Tiempo de interpolación para el movimiento
    public LayerMask layerObstaculos;
    public Animator animator;
    public PlayerInventory playerInventory;

    private bool moviendose = false;
    private bool rotando = false;
    private Vector3 moveDirection; // Dirección de movimiento
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (moviendose || rotando) return;

        // Input para moverse en las direcciones cardinal
        if (Input.GetKeyDown(KeyCode.W))
        {
            Mover(Vector3.up);
            playerInventory._direccion = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Mover(Vector3.left);
            playerInventory._direccion = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Mover(Vector3.down);
            playerInventory._direccion = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Mover(Vector3.right);
            playerInventory._direccion = Vector3.right;
        }
/*
        if (Input.GetKeyUp(KeyCode.W))
         {
            animator.SetBool("isMoving", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isMoving", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isMoving", false);
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("isMoving", false);
            }
        }*/
    }

    void Mover(Vector3 _direccion)
    {
        moveDirection = _direccion;

        // Comprobar obstáculos en la dirección de movimiento
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, _direccion, 1, layerObstaculos);
        if (hit2d.collider != null)
        {
            // Comprobar si es un objeto empujable
            if (hit2d.transform.GetComponent<Empujable>())
            {
                Empujable empujable = hit2d.transform.GetComponent<Empujable>();
                if (empujable.PuedeMoverse(_direccion, lerpTime))
                {
                    StartCoroutine(Moverse(_direccion));
                }
                else
                {
                    Debug.Log("Hay una pared u obstáculo que impide el movimiento.");
                }
            }
        }
        else
        {
            StartCoroutine(Moverse(_direccion));
        }
    }

    IEnumerator Moverse(Vector3 _direccion)
    {
        animator.SetBool("isMoving", true);
        moviendose = true;

        Vector3 desde = transform.position;
        Vector3 hacia = desde + _direccion;
        float t = 0;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            float porcentaje = t / lerpTime;

            // Mover gradualmente
            transform.position = Vector3.Lerp(desde, hacia, porcentaje);

            yield return null;
        }

        // Ajustar posición exacta y finalizar movimiento
        transform.position = hacia;
        animator.SetBool("isMoving", false);
        moviendose = false;

        // Actualizar animaciones después del movimiento
        ActualizarAnimaciones(moveDirection);
    }

    void ActualizarAnimaciones(Vector3 direccion)
    {
        // Ajustar animaciones basadas en la dirección de movimiento
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", direccion.x);
        animator.SetFloat("moveY", direccion.y);
    }
}
