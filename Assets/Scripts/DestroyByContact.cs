using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private GameObject playerExplosion = null;
    
    private GameController gameController;

    void Start(){
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null){
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null){
            Debug.Log("Cannot find 'GameController' object");
        }
    }


    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other) {

        if (other.tag == "Boundary") {
            return;
        }

        if (other.tag == "Player") {
            createPlayerExplosion();
        }

        if (other.tag == "Weapon"){
            gameController.addScoreAsteroid();
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