using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
	//The bullet's position and velocity
	private Vector3 bulletPosition;
	public Vector3 velocity;

	//Details about the camera
	private Camera cam;
	private float height;
	private float width;

	// Start is called before the first frame update
	void Start()
    {
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;
		bulletPosition = transform.position;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		// Draw the bullet at that position
		bulletPosition += velocity;

		//Keeps the bullet within the bounds of the camera
		if (bulletPosition.x > width)
		{
			bulletPosition.x = -width;
		}
		else if (bulletPosition.x < -width)
		{
			bulletPosition.x = width;
		}

		if (bulletPosition.y > height)
		{
			bulletPosition.y = -height;
		}
		else if (bulletPosition.y < -height)
		{
			bulletPosition.y = height;
		}

		gameObject.transform.position = bulletPosition;
	}
}
