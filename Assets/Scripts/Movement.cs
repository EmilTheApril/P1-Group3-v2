using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Player movement variables
    public float speed;
    public float turnSpeed;

    //Player movement area
    public Vector2 minMaxY;
    public Vector2 minMaxX;

    //Players rigidbody
    private Rigidbody2D rb;

    void Start()
    {
        //Sets rb to the players rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is used when using physics. In this case, when using rigidbody
    void FixedUpdate()
    {
        //Calls the Move() function every frame.
        Move();
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    public void Move()
    {
        //The directions we want the player to move (-1 or 1) left or right.
        float dir = 0;

        //GetAxisRaw gives (-1 or 1) right or left, when right or left key is pressed.
        //This makes sure, we only set dir, when a key is pressed.
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //GetAxis gives (-1f to 1f) mening it can be a 0.53 or -0.78. It just makes the player move more smoothly
            //We set dir to the float we get when a key is pressed.
            dir = Input.GetAxis("Horizontal");
        }

        //Checks if the player is on the screen. If the players positions is outside minMaxX.(x/y) or minMaxY.(x/y)
        //then it will make the player turn towards the center of the screen.
        if ((transform.position.y < minMaxY.y && transform.position.y > minMaxY.x) && (transform.position.x > minMaxX.x && transform.position.x < minMaxX.y))
        {
            //Rotates player when you press left or right.
            transform.Rotate(0, 0, turnSpeed * -dir);
        } 
        else
        {
            //Calls a function that turns the player when outside the screen.
            RotatePlayer();
        }

        //Makes the player move the direction it is facing, with the speed we have set.
        rb.velocity = transform.up * speed * Time.deltaTime;
    }


    /// <summary>
    /// Rotates the player towards the center
    /// </summary>
    public void RotatePlayer()
    {
        //target position - player position, gives a vector facing the direction we want the player to go.
        var dir = Vector3.zero - transform.position;

        //Converts the vector (dir) to an angle.
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //Converts the angle into an Quaternion, which is (x, y, z) rotations. Like seen in the inspector.
        Quaternion targetAngle = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //Rotates the player towards the new Quaternion angle, at a given speed.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, turnSpeed);
    }
}
