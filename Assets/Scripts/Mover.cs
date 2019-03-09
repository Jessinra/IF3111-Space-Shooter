using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rigidBody;
    
    [SerializeField] private float speed = 0.0F;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = transform.forward * speed;
    }
}
