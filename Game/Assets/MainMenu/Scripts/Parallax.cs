using UnityEngine;

/// <summary>
/// Class that deals with the parallax effect (ednless scrolling)
/// </summary>
public class Parallax : MonoBehaviour {

	private float spriteLenght, spritePosition; // of the sprite
	public GameObject camera; // reference to main camera
	public float parallexEffect; // amount of parallax effect

	void Start () {
		spritePosition = transform.position.x; // start postion
		spriteLenght = GetComponent<SpriteRenderer>().bounds.size.x; // lenght of sprite
	}
	
	void FixedUpdate () {

        float distanceInWorld = (camera.transform.position.x * parallexEffect); // how far moved in world space
        float distanceFromCam = (camera.transform.position.x * (1 - parallexEffect)); // how far moved relative to the camera
		
		transform.position = new Vector3(spritePosition + distanceInWorld, transform.position.y, transform.position.z); // move the camera in the x direction

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
