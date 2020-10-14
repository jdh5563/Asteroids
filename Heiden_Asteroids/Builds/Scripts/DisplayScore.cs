using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    //The player's score
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //Displays the player's score
        scoreText.text += CheckCollisions.score.ToString();
    }
}
