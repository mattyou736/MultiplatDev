using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float time = 0;

    public Text timeText, lapText, scoreText, boostText,speedText;
    public int laps = 1;
    public int score = 0;

    private Car player;

    public string lvlToStart;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Car>();
    }

    // Update is called once per frame
    void Update ()
    {

        string timemin = ((int)time / 60).ToString();

        string timeSeconds = ((int)time % 60).ToString();

        time += Time.deltaTime;

        timeText.text = timemin + ":" + timeSeconds;
        lapText.text = laps.ToString() + "/3";
        scoreText.text = score.ToString();
        boostText.text = player.boostMeterValue.ToString();
        speedText.text = player.speed.ToString();

        if (laps >= 4)
        {
            SceneManager.LoadScene(lvlToStart);
        }
    }
}
