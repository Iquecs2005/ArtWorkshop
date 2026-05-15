using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicoMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [Header("Move Stats")]
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float moveAcceleration;
    [SerializeField] float moveDesacceleration;

    private Rigidbody2D rb;

    private Vector2 moveDirection;

    private void Start()
    {
        rb = playerController.GetRigidbody();
    }

    private void FixedUpdate()
    {
        //Calcula a velocidade desejada
        Vector2 targetSpeed = moveDirection * maxMoveSpeed;

        //Calcula a diferenca de velocidade entre a atual e a desejada
        Vector2 speedDif = targetSpeed - rb.velocity;

        //Decide a taxa de acceleracao/desaceleracao dependendo se o player quer parar completamente
        float accelRate;
        if (targetSpeed.magnitude > 0.01f)
        {
            accelRate = moveAcceleration;
        }
        else
        {
            accelRate = moveDesacceleration;
        }

        //Aplica a for�a
        rb.AddForce(speedDif * accelRate);
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }
}
