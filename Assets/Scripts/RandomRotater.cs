using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotater : MonoBehaviour
{
    [SerializeField] private float tumble = 0.0F;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
