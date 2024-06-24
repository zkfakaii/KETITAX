using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ramp : MonoBehaviour
{
    public Vector2 rampDirection; // Dirección en la que la rampa empuja al jugador
    public float rampDistance = 1.0f; // Distancia que el jugador recorrerá en la rampa
    public Color gizmoColor = Color.blue; // Color del Gizmo

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(playerMovement.MoveAlongRamp(rampDirection, rampDistance));
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rampDirection.normalized * rampDistance);
    }
}
