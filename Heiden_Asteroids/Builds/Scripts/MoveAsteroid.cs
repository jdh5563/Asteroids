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

	//Details about the camera
	private Camera cam;
	private float height;
	private float width;

	// Start is called before the first frame update
	void Start()
    {
		//Asteroids have a random position and velocity;
        asteroidPosition = transform.position;
        asteroidVelocity = new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));

		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;
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

		//Draws the asteroid at that position
        asteroidPosition += asteroidVelocity;

		//Keeps the asteroid within the bounds of the camera
		if (asteroidPosition.x > width)
		{
			asteroidPosition.x = -width;
		}
		else if (asteroidPosition.x < -width)
		{
			asteroidPosition.x = width;
		}

		if (asteroidPosition.y > height)
		{
			asteroidPosition.y = -height;
		}
		else if (asteroidPosition.y < -height)
		{
			asteroidPosition.y = height;
		}

		transform.position = asteroidPosition;
    }
}
