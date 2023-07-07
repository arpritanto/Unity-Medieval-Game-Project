using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public static int numberOfScore;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numberOfScore = PlayerPrefs.GetInt("NumberOfScore", 0);
        scoreText.text = "Score : " + numberOfScore.ToString();
    }
}
