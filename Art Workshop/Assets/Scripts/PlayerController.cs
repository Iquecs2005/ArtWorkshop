using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public Rigidbody2D GetRigidbody() 
    {
        return rb;
    }
}
