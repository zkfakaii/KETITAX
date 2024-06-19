using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    public GameObject fuegoPausado; // Referencia al fuego en estado Pausado
    public GameObject fuegoReanudado; // Referencia al fuego en estado Reanudado

    void Start()
    {
        // Al inicio, activar el fuego en estado Pausado y desactivar el fuego en estado Reanudado
        fuegoPausado.SetActive(true);
        fuegoReanudado.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Cambiar al estado Reanudado cuando el bullet colisiona
            fuegoPausado.SetActive(false);
            fuegoReanudado.SetActive(true);

            // Destruir el bullet
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        // Destruir el fuego en estado Reanudado después de un tiempo
        if (fuegoReanudado.activeSelf)
        {
            Destroy(gameObject, 2f); // Destruir después de 2 segundos (ajustar según sea necesario)
        }
    }
}
