using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveVehicle : MonoBehaviour
{
	// The initial position, direction, and velocity of the car
	Vector3 vehiclePosition = new Vector3(0, 0, 0);
	public Vector3 direction = new Vector3(1, 0, 0);
	public Vector3 velocity = new Vector3(0, 0, 0);

	// Accel vector will calculate the rate of change per second
	Vector3 acceleration = new Vector3(0, 0, 0);
	public float accelerationRate;
	private float decelerationRate;

	// Don’t need a constant speed anymore since the “speed” changes per frame
	// We do need a speed limit!
	public float maximumSpeed;

	// How fast we want the vehicle to turn
	public float turnSpeed;

	//Details about the camera
	private Camera cam;
	private float height;
	private float width;

	//Whether the car is accelerating or not
	private bool isAccelerating = false;

	//The player's lives
	private int lives = 2;
	public Image[] livesImages;

	public bool isInvincible = false;
	private int invincibilityTimer = 150;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;
		decelerationRate = .975f;
	}

	// Update is in charge of receiving input from the user
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			// Rotate the direction vector by 1 degree each frame
			direction = Quaternion.Euler(0, 0, turnSpeed * Time.deltaTime) * direction;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			// Rotate the direction vector by 1 degree each frame
			direction = Quaternion.Euler(0, 0, -turnSpeed * Time.deltaTime) * direction;
		}

		// The car accelerates when the up arrow is pressed and decelerates when it is released
		if (Input.GetKey(KeyCode.UpArrow))
		{
			isAccelerating = true;
		}
		else
		{
			isAccelerating = false;
		}
	}

	// FixedUpdate is in charge of setting up movement uniformly (not framerate dependent)
	private void FixedUpdate()
	{
		if (isInvincible)
		{
			if (InvincibilityFrames() == 0)
			{
				invincibilityTimer = 150;
				isInvincible = false;
				GetComponent<SpriteRenderer>().enabled = true;
			}
		}

		// Calculate the acceleration vector
		acceleration = direction * accelerationRate;

		if (isAccelerating)
		{
			// Add acceleration to velocity
			velocity += acceleration;
		}
		else
		{
			// Multiply velocity by the deceleration rate
			velocity *= decelerationRate;
		}

		// Limit the velocity so it doesn’t move too quickly
		velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);

		// Draw the vehicle at that position
		vehiclePosition += velocity;

		//Keeps the vehicle within the bounds of the camera
		if (vehiclePosition.x > width)
		{
			vehiclePosition.x = -width;
		}
		else if (vehiclePosition.x < -width)
		{
			vehiclePosition.x = width;
		}

		if (vehiclePosition.y > height)
		{
			vehiclePosition.y = -height;
		}
		else if (vehiclePosition.y < -height)
		{
			vehiclePosition.y = height;
		}

		transform.position = vehiclePosition;

		// Set the vehicle’s rotation to match the direction
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}

	/// <summary>
	/// Respawns the ship after it dies. If the ship is out of lives, it is game over
	/// </summary>
	public void Respawn()
	{
		if (!isInvincible)
		{
			isInvincible = true;

			//Respawns the ship in the middle of the screen 
			if (lives > 0)
			{
				livesImages[lives].enabled = false;
				vehiclePosition = Vector3.zero;
				velocity = Vector3.zero;
				direction = Vector3.right;
				lives--;
			}

			//Loads the game over screen
			else
			{
				SceneManager.LoadScene(2);
			}
		}
	}

	private int InvincibilityFrames()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (invincibilityTimer % 10 == 0)
		{
			sprite.enabled = !sprite.enabled;
		}

		return invincibilityTimer--;
	}
}
