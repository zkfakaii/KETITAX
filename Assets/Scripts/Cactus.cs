using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour

{
    public float stateDuration = 1.0f; // Duración de cada estado
    public int damage = 20; // Daño que inflige al jugador

    private enum State { Cross, InterX, X, InterCross }
    private State currentState;
    private float stateTimer;

    private Collider2D[] colliders;

    void Start()
    {
        currentState = State.Cross;
        stateTimer = stateDuration;
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            ChangeState();
            stateTimer = stateDuration;
        }

        RotateTowardsState();
    }

    void ChangeState()
    {
        switch (currentState)
        {
            case State.Cross:
                currentState = State.InterX;
                SetCollidersActive(false);
                break;
            case State.InterX:
                currentState = State.X;
                SetCollidersActive(true);
                break;
            case State.X:
                currentState = State.InterCross;
                SetCollidersActive(false);
                break;
            case State.InterCross:
                currentState = State.Cross;
                SetCollidersActive(true);
                break;
        }
    }

    void RotateTowardsState()
    {
        float targetAngle = 0;

        switch (currentState)
        {
            case State.Cross:
                targetAngle = 0;
                break;
            case State.InterX:
                targetAngle = 45;
                break;
            case State.X:
                targetAngle = 90;
                break;
            case State.InterCross:
                targetAngle = 135;
                break;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), 360 * Time.deltaTime);
    }

    void SetCollidersActive(bool active)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = currentState == State.Cross || currentState == State.X ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f); // Representa el eje del torniquete

        Vector3[] directions = {
            Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            new Vector3(1, 1, 0).normalized, new Vector3(-1, 1, 0).normalized,
            new Vector3(-1, -1, 0).normalized, new Vector3(1, -1, 0).normalized
        };

        foreach (var direction in directions)
        {
            Gizmos.DrawRay(transform.position, direction * 1.5f); // Ajustar la longitud según sea necesario
        }
    }
}
