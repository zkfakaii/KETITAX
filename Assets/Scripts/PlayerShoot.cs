using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletMaxDistance = 10f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            Vector3 shootDirection = DetermineShootDirection();
            bulletScript.SetDirection(shootDirection);
            bulletScript.SetMaxDistance(bulletMaxDistance);
            bulletScript.SetPlayerTransform(transform);
        }
    }

    Vector3 DetermineShootDirection()
    {
        float lastMoveX = animator.GetFloat("lastMoveX");
        float lastMoveY = animator.GetFloat("lastMoveY");

        if (lastMoveX != 0)
        {
            return new Vector3(lastMoveX, 0, 0);
        }
        else if (lastMoveY != 0)
        {
            return new Vector3(0, lastMoveY, 0);
        }

        // Si ambos son cero, dispara hacia la derecha por defecto
        return Vector3.right;
    }
}


