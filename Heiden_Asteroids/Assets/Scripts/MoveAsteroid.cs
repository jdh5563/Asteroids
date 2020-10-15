using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
	//The asteroid's position and velocity
    private Vector2 asteroidPosition;
    public Vector2 asteroidVelocity;

	//The asteroid's stage
	public int stage;

	private SpawnAsteroid spawner;

	// Start is called before the first frame update
	void Start()
    {
		//Asteroids have a random position and velocity;
        asteroidPosition = transform.position;
        asteroidVelocity = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));

		spawner = GameObject.Find("GameManager").GetComponent<SpawnAsteroid>();
	}

    // Update is called once per frame
    void Update()
    {
		//Determines if an asteroid is stage 1 or 2
		if(transform.localScale.x == 0.2f)
		{
			stage = 1;
		}
		else
		{
			stage = 0;
		}
    }

	private void FixedUpdate()
	{
		//Draws the asteroid at that position
		asteroidPosition += asteroidVelocity;

		//Keeps the asteroid within the bounds of the camera
		if (asteroidPosition.x > spawner.spawnArea.xMax)
		{
			asteroidPosition.x = spawner.spawnArea.xMin;
		}
		else if (asteroidPosition.x < spawner.spawnArea.xMin)
		{
			asteroidPosition.x = spawner.spawnArea.xMax;
		}

		if (asteroidPosition.y > spawner.spawnArea.yMax)
		{
			asteroidPosition.y = spawner.spawnArea.yMin;
		}
		else if (asteroidPosition.y < spawner.spawnArea.yMin)
		{
			asteroidPosition.y = spawner.spawnArea.yMax;
		}

		transform.position = asteroidPosition;
	}
}
