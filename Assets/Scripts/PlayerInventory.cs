using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasAmmo = false;
    public GameObject bulletPrefab; // Referencia al prefab del proyectil

    public Transform firePointLeft; // Punto desde donde se dispara a la izquierda
    public Transform firePointRight; // Punto desde donde se dispara a la derecha
    public Transform firePointUp; // Punto desde donde se dispara hacia arriba
    public Transform firePointDown; // Punto desde donde se dispara hacia abajo

    public float bulletSpeed = 10f; // Velocidad del proyectil
    public float maxBulletDistance = 10f; // Distancia máxima a la que el proyectil puede llegar

    public Vector3 _direccion;// Dirección en la que el jugador está mirando

  


    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Q) && hasAmmo)
        {
            Debug.Log("Player attempting to shoot");
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Player pressed Q");
        }
    }

    void Shoot()
    {
        Debug.Log("entre");
        Transform selectedFirePoint = null;

        // Determinar la dirección en la que el jugador está mirando
        if (_direccion== Vector3.left)
        {
            selectedFirePoint = firePointLeft;
        }
        else if (_direccion == Vector3.right)
        {
            selectedFirePoint = firePointRight;
        }
        else if  (_direccion == Vector3.up)
        {
            selectedFirePoint = firePointUp;
        }
        else if (_direccion == Vector3.down)
        {
            selectedFirePoint = firePointDown;
        }


        
        if (selectedFirePoint != null)
        {

            GameObject bullet = Instantiate(bulletPrefab, selectedFirePoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>(); // Obtener el script del proyectil
            bulletScript.SetDirection(_direccion); // Establecer la dirección del proyectil
            bulletScript.SetMaxDistance(maxBulletDistance); // Establecer la distancia máxima del proyectil
            bulletScript.SetPlayerTransform(transform); // Pasar la referencia al transform del jugador al proyectil
            hasAmmo = false;
            Debug.Log("Player shot a bullet");
        }
    }

    public void SetShootDirection(Vector3 direction)
    {
        _direccion = direction;
    }
}
