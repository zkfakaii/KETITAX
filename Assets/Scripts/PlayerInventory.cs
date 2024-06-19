using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasAmmo = false;
    public GameObject bulletPrefab; // Referencia al prefab del proyectil
    public Transform firePoint; // Punto desde donde se dispara el proyectil
    public float bulletSpeed = 10f; // Velocidad del proyectil
     
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
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed; // Ajusta la velocidad del proyectil
        hasAmmo = false;
        Debug.Log("Player shot a bullet");
    }
}
