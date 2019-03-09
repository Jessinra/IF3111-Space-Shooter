using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private HazardConfig hazardConfig = null;
    [SerializeField] private ScoreConfig scoreConfig = null;
    [SerializeField] private UITextConfig uiTextConfig = null;

    private int score = 0;
    private bool isGameOver = false;
    private bool enableRestart = false;

    // Start is called before the first frame update
    void Start() {
        scoreConfig.updateScore(score);
        uiTextConfig.hideAll();

        StartCoroutine("spawnWaves");
    }

    void Update() {
        if (enableRestart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator spawnWaves() {

        while (!(isGameOver)) {
            for (int i = 0; i < hazardConfig.hazardPerWave; i++) {
                createHazard();
                yield return new WaitForSeconds(hazardConfig.spawnCooldown);
            }
            hazardConfig.increaseDifficulty();
            yield return new WaitForSeconds(hazardConfig.waveCooldown);
        }
        showRestartScreen();
    }

    private void createHazard() {
        Instantiate(hazardConfig.hazard, hazardConfig.getNewSpawnPoint(), hazardConfig.getSpawnRotation());
    }

    public void gameOver() {
        isGameOver = true;
        uiTextConfig.showGameOver();
    }

    private void showRestartScreen() {
        uiTextConfig.showRestart();
        enableRestart = true;
    }

    public void addScoreAsteroid() {
        addScore(scoreConfig.asteroidScore);
        scoreConfig.updateScore(score);
    }

    private void addScore(int score) {
        this.score += score;
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

[System.Serializable]
public class ScoreConfig {
    public Text scoreText;
    public int asteroidScore;

    public void updateScore(int score) {
        scoreText.text = "Score : " + score;
    }
}

[System.Serializable]
public class UITextConfig {
    public Text restartText;
    public Text gameOverText;

    public void hideAll() {
        restartText.text = "";
        gameOverText.text = "";
    }

    public void showRestart() {
        restartText.text = "Press 'R' for restart";
    }

    public void showGameOver() {
        gameOverText.text = "GAME OVER";
    }
}