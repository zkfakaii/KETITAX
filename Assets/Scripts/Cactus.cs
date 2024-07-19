using System.Collections;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public float crossDuration = 1.0f; // Duración del estado CactusCross
    public float idleDuration = 1.0f;  // Duración del estado CactusIdle

    private enum State { Cross, Idle }
    private State currentState;
    private float stateTimer;
    private Animator animator;

    private Collider2D[] colliders;

    void Start()
    {
        currentState = State.Cross;
        stateTimer = crossDuration;
        colliders = GetComponentsInChildren<Collider2D>();
        animator = GetComponent<Animator>();
        SetAnimationState(); // Inicializar la animación
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            ChangeState();
        }
    }

    void ChangeState()
    {
        switch (currentState)
        {
            case State.Cross:
                currentState = State.Idle;
                stateTimer = idleDuration;
                SetCollidersActive(false);
                break;
            case State.Idle:
                currentState = State.Cross;
                stateTimer = crossDuration;
                SetCollidersActive(true);
                break;
        }

        SetAnimationState();
    }

    void SetAnimationState()
    {
        switch (currentState)
        {
            case State.Cross:
                animator.Play("CactusCross");
                break;
            case State.Idle:
                animator.Play("CactusIdle");
                break;
        }
    }

    void SetCollidersActive(bool active)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
    }
}
