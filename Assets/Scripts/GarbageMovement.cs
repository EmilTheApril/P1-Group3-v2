using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMovement : MonoBehaviour
{
    public Vector3 endPoint; // Used to decide destination of items
    public int garbageSpeed;
    public Rigidbody2D rb;
    public Vector2 dir;

    public Sprite[] sprites;
    void Start()
    {
        //Destroys the object after 60 sec
        Destroy(gameObject, 60f);
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0 , sprites.Length)];
    }

    void FixedUpdate(){
        rb.velocity = dir * garbageSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Destroy")){
            Destroy(gameObject);
        }
    }
}
