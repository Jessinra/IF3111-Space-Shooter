using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject playerExplosion;

    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other) {

        if (other.tag == "Boundary") {
            return;
        }

        if (other.tag == "Player") {
            createPlayerExplosion();
        }

        createExplosion();
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private void createExplosion() {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void createPlayerExplosion() {
        Instantiate(playerExplosion, transform.position, transform.rotation);
    }
}