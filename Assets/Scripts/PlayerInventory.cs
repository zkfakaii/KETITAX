using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasAmmo = false;
    public GameObject bulletPrefab; // Referencia al prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public float maxBulletDistance = 10f; // Distancia máxima a la que el proyectil puede llegar

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
        Vector2 shootDirection = firePoint.right; // Dirección en la que el jugador está mirando
        Debug.Log("Shoot direction: " + shootDirection);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>(); // Obtener el script del proyectil
        bulletScript.SetDirection(shootDirection); // Establecer la dirección del proyectil
        bulletScript.SetMaxDistance(maxBulletDistance); // Establecer la distancia máxima del proyectil
        bulletScript.SetPlayerTransform(transform); // Pasar la referencia al transform del jugador al proyectil
        hasAmmo = false;
        Debug.Log("Player shot a bullet");
    }
}
