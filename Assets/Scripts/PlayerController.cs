using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float tilt;

    [SerializeField] private MovementBorderConfig movementBorderConfig;
    [SerializeField] private ShotConfig shotConfig;

    private Rigidbody rigidBody;
    private float nextShot = 0.0F;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (ableToShot()) {
            createShot();
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = movement * speed;

        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, movementBorderConfig.xMin, movementBorderConfig.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, movementBorderConfig.zMin, movementBorderConfig.zMax)
        );

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }

    private bool ableToShot(){
        return Input.GetButton("Fire1") && Time.time > nextShot;
    }

    private void createShot() {
        nextShot = Time.time + shotConfig.shotDelay;
        Instantiate(shotConfig.bullet,
            shotConfig.shotSpawnPoint.position,
            shotConfig.shotSpawnPoint.rotation);

        GetComponent<AudioSource>().Play();
    }
}

[System.Serializable]
public class MovementBorderConfig {
    public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class ShotConfig {
    public GameObject bullet;
    public Transform shotSpawnPoint;
    public float shotDelay = 0.0F;
}