using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Sets up a public variable for the player's movement speed
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Invokes the MoveChar function every frame
        MoveChar();
    }

    //A separate function for moving the character
    void MoveChar()
    {
        //Declares a new vector3 and sets it to the player's position
        Vector3 newPos = transform.position;

        //Gets input from the up arrow
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Adds speed to the Y axis of the newPos variable and makes it framerate independent by multiplying Time.deltaTime
            newPos.y += speed * Time.deltaTime;
        }
        //Gets input from the down arrow
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //Subtracts speed from the Y axis of the newPos variable and makes it framerate independent by multiplying Time.deltaTime
            newPos.y -= speed * Time.deltaTime;
        }
        //Gets input from the right arrow
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Adds speed to the X axis of the newPos variable and makes it framerate independent by multiplying Time.deltaTime
            newPos.x += speed * Time.deltaTime;
        }
        //Gets input from the left arrow
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Subtracts speed from the X axis of the newPos variable and makes it framerate independent by multiplying Time.deltaTime
            newPos.x -= speed * Time.deltaTime;
        }

        //Sets the player's position to the newPos variable that we have changed based on player input
        transform.position = newPos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = collision.gameObject.GetComponent<tileData>().tileSpeed;
    }
}
