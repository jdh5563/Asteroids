using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
	//Fields to set up the asteroids
	public GameObject asteroidPrefab;
	public Sprite[] asteroidSprites;
	public int stage;
	public int asteroidCount;

	//A reference to the collision checking script
	public CheckCollisions collisions;

	//Details about the camera
	private Camera cam;
	private float height;
	private float width;

	// Start is called before the first frame update
	void Start()
	{
		asteroidCount = 0;
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;
	}

	// Update is called once per frame
	void Update()
	{
		//Spawns 5 new asteroids at random positions when the current on screen ones are destroyed
		if (asteroidCount == 0)
		{
			for (int i = 0; i < 5; i++)
			{
				Vector2 randomPosition = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
				GameObject asteroid = Instantiate(asteroidPrefab, randomPosition, Quaternion.identity);
				asteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
				collisions.asteroidRenderers.Add(asteroid.GetComponent<SpriteRenderer>());
				asteroidCount++;
			}
		}
	}

	/// <summary>
	/// Creates 2 new stage 2 asteroids to take the place of the newly destroyed stage 1 asteroid
	/// </summary>
	/// <param name="asteroid">The destroyed stage 1 asteroid</param>
	public void SplitAsteroid(GameObject asteroid)
	{
		for (int i = 0; i < 2; i++)
		{
			GameObject newAsteroid;
			//Instantiates the new asteroid
			if (i == 0)
			{
				newAsteroid = Instantiate(asteroidPrefab,
				new Vector3(asteroid.transform.position.x + 0.01f, asteroid.transform.position.y + 0.01f),
				Quaternion.identity);
			}
			else
			{
				newAsteroid = Instantiate(asteroidPrefab,
				new Vector3(asteroid.transform.position.x - 0.01f, asteroid.transform.position.y - 0.01f),
				Quaternion.identity);
			}

			//Gives the asteroid a velocity similar to its parent asteroid
			newAsteroid.GetComponent<MoveAsteroid>().asteroidVelocity =
				new Vector2(asteroid.GetComponent<MoveAsteroid>().asteroidVelocity.x + Random.Range(-0.01f, 0.01f),
				asteroid.GetComponent<MoveAsteroid>().asteroidVelocity.y + Random.Range(-0.01f, 0.01f));

			//Shrinks the asteroid
			newAsteroid.transform.localScale = new Vector3(0.2f, 0.2f, 1);

			//Gives the asteroid a random sprite
			newAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
			
			//Adds the new asteroid to the asteroid list
			collisions.asteroidRenderers.Add(newAsteroid.GetComponent<SpriteRenderer>());
		}

		//Removes and destroys the old asteroid
		collisions.asteroidRenderers.Remove(asteroid.GetComponent<SpriteRenderer>());
		Destroy(asteroid);
		asteroidCount++;
	}
}
