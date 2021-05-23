using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text score;
    
    void Awake()
    {
        score = GetComponent<Text>();
    }

      
    void Update()
    {
        score.text = "Score : " + ScoreManager.score.ToString();
    }
}
