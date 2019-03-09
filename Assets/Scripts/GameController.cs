using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private HazardConfig hazardConfig;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine("SpawnWaves");
    }

    IEnumerator SpawnWaves() {

        while (true) {
            for (int i = 0; i < hazardConfig.hazardPerWave; i++) {
                createHazard();
                yield return new WaitForSeconds(hazardConfig.spawnCooldown);
            }

            hazardConfig.increaseDifficulty();
            yield return new WaitForSeconds(hazardConfig.waveCooldown);
        }
    }

    private void createHazard() {
        Instantiate(hazardConfig.hazard, hazardConfig.getNewSpawnPoint(), hazardConfig.getSpawnRotation());
    }
}

[System.Serializable]
public class HazardConfig {
    public GameObject hazard;
    public Vector3 spawnPointValues;

    public int hazardPerWave = 5;
    public float spawnCooldown = 0.5F;
    public float waveCooldown = 5.0F;

    public int increaseHazardPerWave = 0;
    public float decreaseSpawnCooldown = 0.0F;
    public float decreaseWaveCooldown = 0.0F;

    private Quaternion spawnRotation = Quaternion.identity;

    public Vector3 getNewSpawnPoint() {

        Vector3 spawnPoint = new Vector3();

        spawnPoint.x = getRandomX();
        spawnPoint.y = spawnPointValues.y;
        spawnPoint.z = spawnPointValues.z;

        return spawnPoint;
    }

    public Quaternion getSpawnRotation() {
        return spawnRotation;
    }

    private float getRandomX() {
        return Random.Range(-spawnPointValues.x, spawnPointValues.x);
    }

    public void increaseDifficulty() {

        hazardPerWave += increaseHazardPerWave;
        spawnCooldown -= decreaseWaveCooldown;
        waveCooldown -= decreaseSpawnCooldown;

    }
}