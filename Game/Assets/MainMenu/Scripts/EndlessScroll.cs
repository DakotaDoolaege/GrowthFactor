using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessScroll : MonoBehaviour { 

    public float scrollingSpeed; // speed of scrolling background (negative to go right)
    Vector2 initialPosition; // begining position

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position; // get current position at start
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollingSpeed, 20); // calculate new position
        transform.position = initialPosition - Vector2.right * newPos; // move to that postion to the right
    }
}
