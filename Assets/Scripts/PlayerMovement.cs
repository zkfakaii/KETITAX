using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // speed of movement
    public float moveDistance = 0.5f; // distance to move each step
    public float pauseTime = 0.1f; // time to pause between steps

    private Vector3 moveDirection; // direction to move
    private bool isMoving = false; // is the character currently moving?

    void Update()
    {
        // get input from the player
        moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }
    }
    public void FixedUpdate()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}