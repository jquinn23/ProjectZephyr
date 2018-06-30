/*
 * Author: Jonathan Quinn
 * Date: 6/29/18
 * This is a simple script to manage the game's menus. It's just a press Space to continue menu, so not very intensive.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

    //Gains access to the game manager to advance scenes
    GameControl controller;

    //Allows the press space to continue string to blink at a fixed update
    public GameObject blinkText;

	// Use this for initialization
	void Start () {
        //Forces the game into windowed mode
        Screen.fullScreen = false;
        //Locates the game manager
        controller = GameObject.Find("GameManager").GetComponent<GameControl>();

        //Starts the blinking function
        InvokeRepeating("BlinkingText", 0.5f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            controller.EndLevel();
        }
	}

    //Causes the given text to blink
    void BlinkingText()
    {
        if(blinkText.activeSelf)
        {
            blinkText.SetActive(false);
        }
        else
        {
            blinkText.SetActive(true);
        }
    }
}
