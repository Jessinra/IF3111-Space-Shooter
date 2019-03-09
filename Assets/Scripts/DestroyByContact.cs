using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private GameObject playerExplosion = null;

    private GameController gameController;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'GameController' object");
        }
    }

    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other) {

        Collider ownCollider = this.GetComponent<Collider>();

        // Item created inside game world boundary
        if (other.CompareTag("Boundary")) {
            return;
        }

        // When enemy spawn weapon
        if(ownCollider.CompareTag("EnemyWeapon") && other.CompareTag("Enemy") || ownCollider.CompareTag("Enemy") && other.CompareTag("EnemyWeapon")){
            return;
        }

        // Gameover when anything hit player
        if (other.CompareTag("Player") || ownCollider.CompareTag("Player")) {
            createPlayerExplosion();
            gameController.gameOver();
        }

        // Scoring
        if (other.CompareTag("Weapon")) {
            if (ownCollider.CompareTag("Asteroid")) {
                gameController.addScoreAsteroid();
            } else if (ownCollider.CompareTag("Enemy")) {
                gameController.addScoreEnemy();
            }
        }

        createExplosion();        
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private void createExplosion() {
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    private void createPlayerExplosion() {
        if (playerExplosion != null) {
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
    }
}