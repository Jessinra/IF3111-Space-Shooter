using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private HazardConfig HazardConfig;

    // Start is called before the first frame update
    void Start() {
        SpawnWaves();
    }

    // Update is called once per frame
    void Update() {

    }

    private void SpawnWaves() {
        createHazard();
    }

    private void createHazard() {
        Instantiate(HazardConfig.hazard, HazardConfig.getNewSpawnPoint(), HazardConfig.getSpawnRotation());
    }
}

[System.Serializable]
public class HazardConfig {
    public GameObject hazard;
    public Vector3 spawnPointValues;
    private Quaternion spawnRotation = Quaternion.identity;

    public Vector3 getNewSpawnPoint() {

        Vector3 spawnPoint = new Vector3();

        spawnPoint.x = getRandomX();
        spawnPoint.y = spawnPointValues.y;
        spawnPoint.z = spawnPointValues.z;

        return spawnPoint;
    }

    public Quaternion getSpawnRotation(){
        return spawnRotation;
    }

    private float getRandomX() {
        return Random.Range(-spawnPointValues.x, spawnPointValues.x);
    }
}