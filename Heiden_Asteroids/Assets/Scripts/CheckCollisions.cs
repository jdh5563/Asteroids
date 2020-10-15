using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCollisions : MonoBehaviour
{
	//The sprite renderers for the ship and asteroids
	public SpriteRenderer shipRenderer;
	public List<SpriteRenderer> asteroidRenderers;
	public List<SpriteRenderer> bulletRenderers;

	//The center coordinate and radius of the ship
	private Vector2 shipCenter;
	private float shipRadius;

	//The text to display score
	public Text scoreText;
	public static int score = 0;

	// Update is called once per frame
	private void Update()
	{
		//Stores the ship's radius and center
		shipRadius = shipRenderer.bounds.extents.x;
		shipCenter = shipRenderer.bounds.center;

		//Checks for collisions between an asteroid and either the ship or a bullet
		ShipToAsteroid();
		BulletToAsteroid();
	}

	/// <summary>
	/// Cycles through all asteroids to determine if a collision has occurred between an asteroid and the ship using circle collision
	/// </summary>
	private void ShipToAsteroid()
	{
		//The asteroids' centers and radii
		Vector2 asteroidCenter;
		float asteroidRadius;

		//Iterates through all asteroids
		for (int i = 0; i < asteroidRenderers.Count; i++)
		{
			asteroidCenter = asteroidRenderers[i].bounds.center;
			asteroidRadius = asteroidRenderers[i].bounds.extents.x;

			float radiiDistance = shipRadius + asteroidRadius;

			//Determines if a collision has occurred, destroying both the ship and asteroid if one has occurred
			if (radiiDistance >
				(Mathf.Pow(shipCenter.x - asteroidCenter.x, 2) +
				Mathf.Pow(shipCenter.y - asteroidCenter.y, 2)))
			{
				if (!shipRenderer.gameObject.GetComponent<MoveVehicle>().isInvincible)
				{
					if (asteroidRenderers[i].gameObject.GetComponent<MoveAsteroid>().stage == 0)
					{
						gameObject.GetComponent<SpawnAsteroid>().SplitAsteroid(asteroidRenderers[i].gameObject);
					}
					else
					{
						Destroy(asteroidRenderers[i].gameObject);
						asteroidRenderers.Remove(asteroidRenderers[i]);
						gameObject.GetComponent<SpawnAsteroid>().asteroidCount--;
					}
				}

				shipRenderer.gameObject.GetComponent<MoveVehicle>().Respawn();

				return;
			}
		}
	}

	/// <summary>
	/// Cycles through all bullets and asteroids to determine if a collision has occurred.
	/// </summary>
	private void BulletToAsteroid()
	{
		//The asteroids' centers and radii
		Vector2 asteroidCenter;
		float asteroidRadius;

		//The bullets' centers and radii
		Vector2 bulletCenter;
		float bulletRadius;

		//The score text
		score = int.Parse(scoreText.text);

		//Iterates through all bullets and asteroids to determine if a collision has occurred
		for (int i = 0; i < bulletRenderers.Count; i++)
		{
			bulletCenter = bulletRenderers[i].bounds.center;
			bulletRadius = bulletRenderers[i].bounds.extents.x;

			for(int j = 0; j < asteroidRenderers.Count; j++)
			{
				asteroidCenter = asteroidRenderers[j].bounds.center;
				asteroidRadius = asteroidRenderers[j].bounds.extents.x;

				float radiiDistance = bulletRadius + asteroidRadius;

				//Determines if a collision has occurred, destroying both the bullet and the asteroid if one has occurred
				if (radiiDistance >
					(Mathf.Pow(bulletCenter.x - asteroidCenter.x, 2) +
					Mathf.Pow(bulletCenter.y - asteroidCenter.y, 2)))
				{
					Destroy(bulletRenderers[i].gameObject);
					bulletRenderers.RemoveAt(i);
					i--;

					if (asteroidRenderers[j].gameObject.GetComponent<MoveAsteroid>().stage == 0)
					{
						gameObject.GetComponent<SpawnAsteroid>().SplitAsteroid(asteroidRenderers[j].gameObject);
						scoreText.text = (score + 20).ToString();
					}
					else
					{
						Destroy(asteroidRenderers[j].gameObject);
						asteroidRenderers.Remove(asteroidRenderers[j]);
						gameObject.GetComponent<SpawnAsteroid>().asteroidCount--;
						scoreText.text = (score + 50).ToString();
					}
					break;
				}
			}
		}
	}
}
