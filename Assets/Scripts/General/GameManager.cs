using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    public string playerName;

    public TMP_Text score;
    float baseScore = 0f;
    float currentScore = 0f;

    public Animator transition;
    public float transitionTime = 1f;

    public GameObject gameOverImage;
    public TMP_Text gameOverScore;

    GameObject plr;

    private void Awake() {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

        SetupDefaults();
    }

    private void Update() {
        currentScore = baseScore + plr.transform.position.z;
        score.text = currentScore.ToString("0");
    }

    public void UpdateBaseScore() {
        baseScore = currentScore;
    }

    void SetupDefaults() {
        plr = GameObject.FindGameObjectWithTag(Tags.player);
    }

    public void GameOver() {
        score.gameObject.SetActive(false);
        gameOverImage.SetActive(true);
        currentScore = (int)currentScore;
        float prevHighscore = PlayerPrefs.GetInt(playerName, 0);
        if (currentScore > PlayerPrefs.GetInt(playerName, 0)) {
            PlayerPrefs.SetInt(playerName, (int)currentScore);
            gameOverScore.text = $"       score: {currentScore} \nhighscore: {currentScore} *new*";
        }
        gameOverScore.text = $"       score: {currentScore} \nhighscore: {prevHighscore}";
    }

    public void Restart() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
