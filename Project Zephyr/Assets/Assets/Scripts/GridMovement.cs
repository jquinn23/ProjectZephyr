/*
 * Original Code: Youtube's TheKingOfZombies
 * Tutorial Link: https://www.youtube.com/watch?v=rQrzBAMhlw0
 * 
 * Heavy Modifications made by: Jonathan R. Quinn
 * Date of Last Modification: 6/26/18
 * 
 * Changes made: Created a pause between movements and fixed movement rigidly along a grid.
 * Additionally, comments were added for readabiity, and a scaling function was added for the grid movement
 * 
 * 6/26/18
 * Added in raycasts to determine if next blocks were available for movement, along with 
 * 4 booleans to hold said information for each direction
 */

using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    /*
     * Enum created for simple modification to allow for sprite directional changes. I'm not using directions in this program, but it will
     * make future use of the script much more streamlined
     */
    public enum DIRECTION { UP, DOWN, LEFT, RIGHT };

    //These four bools will be used to determine if a given direction is available for movement
    public bool moveNorth;
    public bool moveSouth;
    public bool moveEast;
    public bool moveWest;

    //Booleans to prevent the user from moving mid movement
    private bool canMove = true;
    private bool moving = false;

    //Option to control from Unity Editor. Self Descriptive.
    public int speed = 5;
    public int cooldown = 5;

    //Prevents the user from holding down a button and smoothly moving. Gives a broken up feel meant for grid movement
    //Additionally, if animations were used would allow the player to turn in a direction without moving in that direction
    public int buttonCooldown;

    //Represents the target position for the units next movement
    private Vector3 pos;

    //Gives the object a reference for a position to return to upon encountering a collisio.n
    private Vector3 previousPos;
    //This will scale the movement of the object, allowing for the grid to be shrunk down to half units instead of whole, etc
    public float scale = 1f;

    void Start()
    {
        //Sets the target position to the starting position of the object and sets the buttonCooldown to it's public equivalent
        pos = transform.position;
        buttonCooldown = cooldown;
    }

    /*
     * The update function for this class registers the calls the move class, and then performs the movement in question when given the go ahead 
     */
    void Update()
    {
        UpdateSurroundings();
        //Reduces buttonCooldown each frame. When 0 or below, an action can be performed
        buttonCooldown--;

        //Calls the move function only if the game is finished with the previous movement
        if (canMove)
        {
            Move();
        }


        if (moving)
        {
            /*
             * Moves the object towards the target position at a rate based upon the speed option.
             *  Pos will be changed from it's base to the target position further below.           
             */
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

            //If this condition is true, the movement has finished and another movement can be registered after a brief cooldown period. 
            if (transform.position == pos)
            {
                moving = false;
                canMove = true;
                buttonCooldown = cooldown;
            }
        }
    }


    /*
     * This method sends out 4 raycasts to determine what directions the player can move from their current position
     */
    private void UpdateSurroundings()
    {
        //Sends out raycasts in four directions
        RaycastHit2D northCast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up), Color.green, 3f);

        RaycastHit2D southCast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.green, 3f);

        RaycastHit2D westCast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left), 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left), Color.green, 3f);

        RaycastHit2D eastCast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right), Color.green, 3f);

        /*
         * If one of the casts hits something (unactivated blocks are triggers which won't be detected due to physics settings) 
         * then movement in that direction cannot occur
         */
        if(northCast.collider != null)
        {
            moveNorth = false;
        }
        else
        {
            moveNorth = true;
        }

        if(southCast.collider != null)
        {
            moveSouth = false;
        }
        else
        {
            moveSouth = true;
        }

        if (eastCast.collider != null)
        {
            moveEast = false;
        }
        else
        {
            moveEast = true;
        }

        if (westCast.collider != null)
        {
            moveWest = false;
        }
        else
        {
            moveWest = true;
        }
    }

    /*
     * The Move function determines which input (if any) has been given, 
     * and controls when the object is allowed to move taking button cooldowns into account
     */
    private void Move()
    {
        //If the button cooldown hasn't run down yet, the move function doesn't do anything
        if(buttonCooldown <=0)
        {

            if (Input.GetKey(KeyCode.W) && moveNorth)
            {

                /*
                 * If the player's sprite wasn't facing upwards prior to the input, 
                 * changes the character to face that direction and adds a cooldown 
                 * before allowing movement. Only uncomment if this feature is desired
                 * otherwise it will require a double tap for movement.
                 */
                /*
                  if(dir != DIRECTION.UP)
                  {
                      buttonCooldown = setButtonCooldown;
                      dir = DIRECTION.UP;
                  }
                  else
                  {
                      //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                      canMove = false;
                      moving = true;
                      pos += Vector3.Scale(Vector3.up, new Vector3(0, scale, 0));
                  }
                  */

                //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                canMove = false;
                moving = true;
                pos += Vector3.Scale(Vector3.up, new Vector3(0, scale, 0));
            }
            else if(Input.GetKey(KeyCode.S) && moveSouth)
            {
                /*
                 * If the player's sprite wasn't facing upwards prior to the input, 
                 * changes the character to face that direction and adds a cooldown 
                 * before allowing movement. Only uncomment if this feature is desired
                 * otherwise it will require a double tap for movement.
                 */
                /*
                if (dir != DIRECTION.DOWN)
                {
                    buttonCooldown = setButtonCooldown;
                    dir = DIRECTION.DOWN;
                }
                else
                {
                    //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                    canMove = false;
                    moving = true;
                    pos += Vector3.Scale(Vector3.down, new Vector3(0, scale, 0));
                }
                */

                //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                canMove = false;
                moving = true;
                pos += Vector3.Scale(Vector3.down, new Vector3(0, scale, 0));
            }
            else if (Input.GetKey(KeyCode.A) && moveWest)
            {
                /*
                 * If the player's sprite wasn't facing upwards prior to the input, 
                 * changes the character to face that direction and adds a cooldown 
                 * before allowing movement. Only uncomment if this feature is desired
                 * otherwise it will require a double tap for movement.
                 */
                /*
                if (dir != DIRECTION.LEFT)
                {
                    buttonCooldown = setButtonCooldown;
                    dir = DIRECTION.LEFT;
                }
                else
                {
                    //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                    canMove = false;
                    moving = true;
                    pos += Vector3.Scale(Vector3.left, new Vector3(scale, 0, 0));
                }
                */

                //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                canMove = false;
                moving = true;
                pos += Vector3.Scale(Vector3.left, new Vector3(scale, 0, 0));
            }
            else if (Input.GetKey(KeyCode.D) && moveEast)
            {
                /*
                 * If the player's sprite wasn't facing upwards prior to the input, 
                 * changes the character to face that direction and adds a cooldown 
                 * before allowing movement. Only uncomment if this feature is desired
                 * otherwise it will require a double tap for movement.
                 */
                /*
                if (dir != DIRECTION.RIGHT)
                {
                    buttonCooldown = setButtonCooldown;
                    dir = DIRECTION.RIGHT;
                }
                else
                {
                    canMove = false;
                    moving = true;
                    pos += Vector3.Scale(Vector3.right, new Vector3(0, scale, 0));
                }
                */

                //Causes movement to be true, and sets the target pos to be one scaled unit upwards
                canMove = false;
                moving = true;
                pos += Vector3.Scale(Vector3.right, new Vector3(scale, 0, 0));
            }
        }

    }

}
 
 