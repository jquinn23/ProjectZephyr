/*
 * Author: Jonathan R. Quinn
 * This class manages the behavior of the floor tiles in my project.
 * It will allow them to enter into a triggered state, in which they detect if they are being stood on,
 * or if they have been activated/deactivated, so as to create a color change and stop the player
 * from re-entering already entered spaces.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloorScript : MonoBehaviour {

    //Manages the state of the floor, with 0 being deactivated, 1 being stood on, and 2 being activated
    public int state;

    //Will Stall out floor triggers to make the animation look smoother. Must be based on movement speed, so public.
    public int triggerDelay;

    //Will reference the sprite's renderer in order to manipulate the color
    private SpriteRenderer rend;
    
    //Colors for the additional triggered states are held within public variables
    public Color stoodOnColor;
    public Color activatedColor;

    //This is the Collider for the floor object
    private Collider2D coll;

    // Use this for initialization
    void Start () {
        //Fetch the SpriteRenderer from the GameObject
        rend = GetComponent<SpriteRenderer>();

        //Initialize the collider
        coll = GetComponent<Collider2D>();
	}


    //Changes the state to stood on while the player stands on it.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Makes sure it's the player we're colliding with
        if(collision.gameObject.tag == "Player")
        {
            //Will stall the changing of states until the tile is completely covered
            for(int j = 0; j < triggerDelay; j++)
            {

            }
            if (state == 0)
            {
                state = 1;
            }
        }
        
    }

    //Changes the state from stood on to activated when the player leaves it
    private void OnTriggerExit2D(Collider2D collision)
    {
            if (state == 1)
            {
                state = 2;
            }
    }
    // Update is called once per frame

    //When the state is changed, ensures the colors change with it
    private void stateChange()
    {
        if (state == 1)
        {
            rend.color = stoodOnColor;
        }
        else if(state == 2)
        {
            coll.isTrigger = false;
            rend.color = activatedColor;
        }
    }

    //Calls the stateChange method to check when the state changes.
    void Update () {
        stateChange();
	}
}
