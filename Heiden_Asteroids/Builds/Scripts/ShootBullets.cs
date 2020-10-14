using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
	//References to the bullet prefab for spawning and the ship for location
    public GameObject bulletPrefab;
    public GameObject ship;

	//A reference to the collision checking script
	public CheckCollisions collisions;

	//The speed of the bullet
	private Vector3 bulletVelocity = Vector3.zero;

	//A delay so bullets can't be spammed
	private int delay = 0;

	// Update is called once per frame
	void Update()
    {
		//If the cooldown timer is done and the user presses space, a bullet is fired from the ship
		if (Input.GetKeyDown(KeyCode.Space) && delay == 0)
		{
			//Spawns a bullet with a velocity aimed towards the direction the ship is facing
			bulletVelocity = Vector3.Normalize(ship.GetComponent<MoveVehicle>().direction) * 0.25f;

			GameObject bullet = Instantiate(bulletPrefab, 
				new Vector2(ship.transform.position.x, ship.transform.position.y), 
				Quaternion.identity);
			collisions.bulletRenderers.Add(bullet.GetComponent<SpriteRenderer>());
			collisions.bulletRenderers[collisions.bulletRenderers.Count - 1].gameObject.GetComponent<MoveBullet>().velocity = bulletVelocity;

			//Destroys the oldest on screen bullet if there are more than 5 on screen at the same time to limit clutter
			if(collisions.bulletRenderers.Count > 5)
			{
				SpriteRenderer bulletToRemove = collisions.bulletRenderers[0];
				collisions.bulletRenderers.Remove(bulletToRemove);
				Destroy(bulletToRemove.gameObject);
			}

			delay = 1;
		}
	}

	/// <summary>
	/// Updates the delay timer at a uniform rate
	/// </summary>
	private void FixedUpdate()
	{
		if (delay != 0)
		{
			RunTimer();
		}
	}

	/// <summary>
	/// Increments a delay timer so bullets can't be spammed
	/// </summary>
	private void RunTimer()
	{
		if(delay < 26)
		{
			delay++;
		}
		else
		{
			delay = 0;
		}
	}
}
