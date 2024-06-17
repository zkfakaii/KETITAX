using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour

{
    public int frutoTemporalCount = 0;
    public GameObject frutoTemporalPrefab; // Referencia al prefab del proyectil
    public Transform spawnPoint; // Punto desde donde se lanza el proyectil

    private GameObject currentFrutoTemporal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && frutoTemporalCount > 0 && currentFrutoTemporal == null)
        {
            ShootFrutoTemporal();
        }
    }

    public void AddFrutoTemporal()
    {
        frutoTemporalCount++;
        Debug.Log("Fruto Temporal recogido. Total: " + frutoTemporalCount);
    }

    void ShootFrutoTemporal()
    {
        Vector3 shootDirection = (Vector3)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnPoint.position);
        shootDirection.z = 0; // Asegurarse de que la dirección esté en el plano 2D
        currentFrutoTemporal = Instantiate(frutoTemporalPrefab, spawnPoint.position, Quaternion.identity);
        currentFrutoTemporal.GetComponent<FrutoTemporal>().SetDirection(shootDirection, transform);
        frutoTemporalCount--;
    }
}
