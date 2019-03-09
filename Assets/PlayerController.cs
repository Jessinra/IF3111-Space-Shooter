using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rigidBody;
    [SerializeField] private float speed;
    [SerializeField] private float tilt;
    [SerializeField] private MovementBorder movementBorder;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = movement * speed;

        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, movementBorder.xMin, movementBorder.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, movementBorder.zMin, movementBorder.zMax)
        );

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }
}

[System.Serializable]
public class MovementBorder {
    public float xMin, xMax, zMin, zMax;
}