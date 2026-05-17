using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CltiltMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Header("Move Stats")]
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float moveAcceleration;
    [SerializeField] float moveDesacceleration;

    private Vector2 moveDirection;

    private void FixedUpdate()
    {
        //Calcula a velocidade desejada
        Vector2 targetSpeed = moveDirection * maxMoveSpeed;

        //Calcula a diferenca de velocidade entre a atual e a desejada
        Vector3 speedDif = new Vector3(targetSpeed.x, 0, targetSpeed.y) - rb.velocity;
        speedDif.y = 0;

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
