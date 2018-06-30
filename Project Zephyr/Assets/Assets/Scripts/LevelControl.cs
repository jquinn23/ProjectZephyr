/*
 * Author: Jonathan R. Quinn
 * Date: 6/26/18
 * This script checks to see if the level win condition has been met
 * and acts accordingly
 * 
 * Edit: 6/27/18 Made changes to how floor tiles are imported into the script.
 * Now using children of the tilemap, as opposed to manual insertion
 * 
 * Edit: 6/27/18 Added functionality for Win and Loss Text to be displayed
 * depending on the circumstances
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour {
    
    //Boolean that shows the rest of the program if the level is complete or not
    public bool levelComplete = false;

    //This is a list of all the floor objects in a level.
    private List<FloorScript> floorActivated = new List<FloorScript>();

    //This is the tilemap under which the floor prefabs are children
    public GameObject floorTilemap;

    //This is an int indicating the number of entered spaces.
    int numberIncomplete = 0;

    //This is the controller for the overall game
    public GameControl controller;

    //Used to prevent players for failing for first 10 frames, fixes loading issues
    private int lagTime = 10;

    //Variables in reference to the player's current state, used to detect game over
    public GameObject player;
    private GridMovement playerMove;

    //These variables hold the text used on win and loss, to be enabled on the given condition
    public GameObject winText;
    public GameObject winText2;
    public GameObject loseText;
    public GameObject loseText2;

    private FloorScript[] floors;

    void Start()
    {
        //Ensures all texts are disabled
        winText.SetActive(false);
        winText2.SetActive(false);
        loseText.SetActive(false);
        loseText2.SetActive(false);

        //Will cache the player's movement script to read it's movable states
        playerMove = player.GetComponent<GridMovement>();

        //Loads the controller into the levelmanager
        controller = GameObject.Find("GameManager").GetComponent<GameControl>();

        //Locates the child floorscripts under the tilemap and loads them into floorActivated
        floors = floorTilemap.GetComponentsInChildren<FloorScript>();

        foreach(FloorScript floorTile in floors)
        {
            floorActivated.Add(floorTile);
        }
        
    }

	// Update is called once per frame
	void Update () {
        //Player cannot fail for first few frames, prevents loading issues.
        lagTime--;

        /*
         * If the player wants to restart the level, all they have to do is press R
         */
        if(Input.GetKeyDown(KeyCode.R))
        {
            controller.RestartLevel();
        }
        /* 
         * Checks each FloorScript to see if it has been activated yet.
         * if it hasn't, it adds to the count
         */
        foreach(FloorScript floor in floorActivated)
        {
            if(floor.state == 0)
            {
                numberIncomplete++;
            }
        }
        
        //If no tiles are left untouched, indicates the level is finished
        if(numberIncomplete == 0)
        {
            LevelComplete();
        }

        //If the player has not completed the level and cannot move, the level has been failed
        if(!(playerMove.moveNorth || playerMove.moveSouth || playerMove.moveEast || playerMove.moveWest || levelComplete) && lagTime <= 0)
        {
            LevelFailed();
        }

        //Resets numberIncomplete for another pass
        numberIncomplete = 0;
	}

    private void LevelComplete()
    {
        levelComplete = true;
        winText.SetActive(true);
        winText2.SetActive(true);

        //If the level is complete and the user presses space, end the level
        if(Input.GetKeyDown(KeyCode.Space))
        {
            controller.EndLevel();
        }
    }

    //Makes the fail text visible
    private void LevelFailed()
    {
        loseText.SetActive(true);
        loseText2.SetActive(true);
    }
}
