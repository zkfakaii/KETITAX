using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    private Animator animator;
    private bool isBurning = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Aseg�rate de que la animaci�n est� en el estado pausado al inicio
        animator.SetBool("isBurning", false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Cambiar al estado Reanudado cuando el bullet colisiona
            animator.SetBool("isBurning", true);
            isBurning = true;

            // Destruir el bullet
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        // Destruir el fuego en estado Reanudado despu�s de un tiempo
        if (isBurning)
        {
            StartCoroutine(BurnAndDestroy());
        }
    }

    private IEnumerator BurnAndDestroy()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos (ajustar seg�n sea necesario)
        Destroy(gameObject);
    }
}
