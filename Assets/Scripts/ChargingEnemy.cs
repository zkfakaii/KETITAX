using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    public float detectionWidth = 5f; // Ancho de la zona de detección
    public float detectionHeight = 3f; // Altura de la zona de detección
    public float chargeSpeed = 5f; // Velocidad de embestida
    public float returnSpeed = 3f; // Velocidad de regreso
    public float chargeDistance = 10f; // Distancia máxima de embestida
    public Vector2 chargeDirection = Vector2.right; // Dirección de embestida
    public int damage = 20; // Daño que inflige al jugador
    public float freezeDuration = 2.0f; // Duración del estado congelado

    private Vector3 initialPosition; // Posición inicial del enemigo
    private bool isCharging = false; // ¿Está el enemigo embistiendo?
    private bool isReturning = false; // ¿Está el enemigo regresando a su posición inicial?
   [SerializeField] private bool isFrozen = false; // ¿Está el enemigo congelado?
    private Vector3 chargeTarget; // Objetivo de la embestida

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!isFrozen)
        {
            if (isCharging)
            {
                Charge();
            }
            else if (isReturning)
            {
                ReturnToInitialPosition();
            }
            else
            {
                DetectPlayer();
            }
        }
    }

    void DetectPlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(detectionWidth, detectionHeight), 0);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                isCharging = true;
                chargeTarget = transform.position + (Vector3)(chargeDirection.normalized * chargeDistance);
                break;
            }
                         

        }
    }

    void Charge()
    {
        transform.position = Vector3.MoveTowards(transform.position, chargeTarget, chargeSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, chargeTarget) < 0.01f)
        {
            isCharging = false;
            isReturning = true;
        }
    }

    void ReturnToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
        {
            isReturning = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
     

        if (isCharging && collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
      
    }
    public void Freze() 
        {
        StartCoroutine(FreezeEnemy());
    }

    private IEnumerator FreezeEnemy()
    {
        isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        isFrozen = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(detectionWidth, detectionHeight));
    }
}
