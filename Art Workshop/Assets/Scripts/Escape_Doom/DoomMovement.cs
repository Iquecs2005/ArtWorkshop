using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoomMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [Header("Move Stats")]
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float moveAcceleration;
    [SerializeField] float moveDesacceleration;

    private Rigidbody2D rb;

    private float moveDirection;

    private void Start()
    {
        rb = playerController.GetRigidbody();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        float desiredMoveDir = moveDirection;

        float desiredSpeed = desiredMoveDir * maxMoveSpeed;
        float speedDif = desiredSpeed - rb.velocity.x;

        float accelRate;
        if (desiredSpeed * rb.velocity.x >= 0)
        {
            if (Mathf.Abs(desiredSpeed) >= Mathf.Abs(rb.velocity.x))
            {
                accelRate = moveAcceleration;
            }
            else
            {
                accelRate = moveDesacceleration;
            }
        }
        else
        {
            accelRate = moveDesacceleration;
        }

        rb.AddForce(Vector2.right * speedDif * accelRate);
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection.x;
    }
}
