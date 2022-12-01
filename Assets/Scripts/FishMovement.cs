using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public Vector2 speed;       //min and max speed
    public Sprite[] sprites;    //Sprites
    public bool moveLeft;       //Move dir
    private float finalSpeed;   //A random speed between the min and max
    private Rigidbody2D rb;     //Rigidbody
    private int x;              //Is either 1 or -1. Used in rb.velocity to decide which dir to move

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)]; //Sets the sprite to and random sprite from the sprite array
        FlipFish(); //Flips the fish sprite if necessary

        finalSpeed = Random.Range(speed.x, speed.y); //Decides the random speed between min and max
    }

    public void FixedUpdate()
    {
        Move(x);
    }

    public void Move(float dir)
    {
        rb.velocity = dir * transform.right * finalSpeed * Time.deltaTime;
    }

    public void FlipFish()
    {
        //Checks if the fish needs to be flipped.
        if (!moveLeft)
        {
            x = 1;

            //Flips the fish
            transform.localScale = new Vector3(-x, 1, 1);
        }
        else x = -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
