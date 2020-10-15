using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	public void LoadMenu()
	{
		SceneManager.LoadScene(0);
	}

	//Loads the main game scene
    public void LoadGame()
	{
        SceneManager.LoadScene(1);
	}

	//Exits the game
	public void QuitGame()
	{
		Application.Quit();
	}
}
