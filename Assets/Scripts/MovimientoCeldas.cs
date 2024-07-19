using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCeldas : MonoBehaviour
{

    public float lerpTime;
    bool moviendose, rotando;
    public LayerMask layerObstaculos;
    public Vector3 moveDirection; // Dirección de movimiento
    private bool isMoving = false; // ¿Está el personaje moviéndose actualmente?
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInventory playerInventory;

    public PlayerInventory moveDirectionPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // Solo aceptar input si no está moviéndose y no está bloqueado
        
      
    


    
        
       
        { 
        
             if(Input.GetKeyDown(KeyCode.W))
            {
             Mover(Vector3.up);
                moveDirectionPoint._direccion = Vector3.up;

            }
             if (Input.GetKeyDown(KeyCode.A))
            {
             Mover(Vector3.left);
                moveDirectionPoint._direccion = Vector3.left;
            }
             if (Input.GetKeyDown(KeyCode.S))
            {
             Mover(Vector3.down);
                moveDirectionPoint._direccion = Vector3.down;
            }
             if (Input.GetKeyDown(KeyCode.D))
            {
             Mover(Vector3.right);
                moveDirectionPoint._direccion = Vector3.right;
            }

        }

    }

    void Mover(Vector3 _direccion)
    {   
        if(!rotando)
        {
            StartCoroutine(Rotar(_direccion));
        }

        if (moviendose) return;
        // chequear si hay obstáculos

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, _direccion, 1, layerObstaculos);
        if(hit2d.collider!= null)
        {
            // chequear si hay obstáculos rompibles o empujables
            if(hit2d.transform.GetComponent<Empujable>())
            {
                Empujable empujable = hit2d.transform.GetComponent<Empujable>();
                if(empujable.PuedeMoverse(_direccion, lerpTime))
                {
                    StartCoroutine(Moverse(_direccion));
                }
                else
                {
                    print("hay pared");
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
        moviendose = true;
        Vector3 desde = transform.position;
        Vector3 hacia = desde + _direccion;
        float t = 0;

        while(t < lerpTime)
        {
            t += Time.deltaTime;
            float porcentaje = t / lerpTime;
            transform.position = Vector3.Lerp(desde, hacia, porcentaje);
            yield return null;
        }

        moviendose = false;
    }

    IEnumerator Rotar(Vector3 _direccion)
    {
        rotando = true;

        float t = 0;

        Vector3 startDirection = transform.up;
        Vector3 haciaDirection = _direccion;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            float porcentaje = t / lerpTime;
            transform.up = Vector3.Lerp(startDirection, haciaDirection, porcentaje);
            yield return null;
        }

        rotando = false;
    }
}
