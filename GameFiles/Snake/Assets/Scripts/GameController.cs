using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Area
{
    public float yMin, yMax, xMin, xMax;
}

public class GameController : MonoBehaviour {

    public Area area;

    public GameObject food;

    public int score = 0;
    private float time = 0;

    public Text scoreText;
    public Text timeText;
    public Text gameText;

    private PlayerController playerController;
    public GameObject player;

    public bool gameOver = false;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + time;
        gameText.text = "Snake Clone";
    }

    private void Update()
    {
        if (gameOver == false)
        {
            trackTime();
        }
        else
        {
            gameOverText();
        }
    }

    public void generateFood()
    {
        float yVal = Mathf.RoundToInt(Random.Range(area.yMin, area.yMax)) + 0.5f;
        float xVal = Mathf.RoundToInt(Random.Range(area.xMin, area.xMax)) + 0.5f;
        Instantiate(food, new Vector3(xVal, yVal, 0), Quaternion.identity);
    }

    public void addScore()
    {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }

    public void trackTime()
    {
        time = time + Time.deltaTime;
        timeText.text = "Time: " + Mathf.RoundToInt(time);
    }

    public void gameOverText()
    {
        gameText.text = "Game Over. Press 'R' to Restart";
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

}
