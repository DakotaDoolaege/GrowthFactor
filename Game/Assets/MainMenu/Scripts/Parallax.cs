using UnityEngine;

/// <summary>
/// Class that deals with the parallax effect (ednless scrolling)
/// </summary>
public class Parallax : MonoBehaviour
{

	private float spriteLenght, spritePosition; // of the sprite
	public new GameObject camera; // reference to main camera
	public float parallaxEffect; // amount of parallax effect

	void Start ()
    {
		spritePosition = transform.position.x; // start postion
		spriteLenght = GetComponent<SpriteRenderer>().bounds.size.x; // length of sprite
	}

	void FixedUpdate () {

        float distanceInWorld = (camera.transform.position.x * parallaxEffect); // how far moved in world space
        float distanceFromCam = (camera.transform.position.x * (1 - parallaxEffect)); // how far moved relative to the camera

		transform.position = new Vector2(spritePosition + distanceInWorld, transform.position.y); // move the camera in the x direction

        // make the background repeat
        if (distanceFromCam > spritePosition + spriteLenght)
        {
            spritePosition = spritePosition + spriteLenght;
        }
        else if (distanceFromCam < spritePosition - spriteLenght)
        {
            spritePosition = spritePosition - spriteLenght;
        }
	}

}
