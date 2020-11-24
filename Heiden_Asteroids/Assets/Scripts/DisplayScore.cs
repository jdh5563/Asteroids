using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
	//The player's score
	public Text scoreText;
	public Text highscoreText;

	// The player's highscore
	private int highscore;

	// Start is called before the first frame update
	void Start()
	{
		LoadGame();

		//Displays the player's score
		scoreText.text += CheckCollisions.score.ToString();

		if(CheckCollisions.score > highscore)
		{
			highscore = CheckCollisions.score;
			SaveGame();
		}

		highscoreText.text += highscore.ToString();
	}

	/// <summary>
	/// Saves data to the computer's registry as plain text
	/// </summary>
	void SaveGame()
	{
		// Saves data that is identifiable by its name
		PlayerPrefs.SetInt("highscore", highscore);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Loads data from a specific file in the computer's registry
	/// </summary>
	void LoadGame()
	{
		// Attempts to load saved data if at least one
		// of the pieces of saved data exists
		if (PlayerPrefs.HasKey("highscore"))
		{
			highscore = PlayerPrefs.GetInt("highscore");
		}

		// If the data doesn't exist, set the highscore to 0
		else
			highscore = 0;
	}
}
