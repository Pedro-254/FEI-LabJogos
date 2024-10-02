using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public static int scorecount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Score: " + scorecount);
        scoreText.text = "Score: " + scorecount;

        if (scorecount >= 100)
        {
            scorecount = 0;
            SceneManager.LoadScene("Win");
        }
    }

    public static void AddScore(int score)
    {
        scorecount += score;
    }
}
