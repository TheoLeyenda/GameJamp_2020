using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    public float speed = 6;
    private float auxGravityScaler;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

        }
        else if (other.tag == "Player" && Input.GetKey(KeyCode.S))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);

        }
        else if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, other.GetComponent<Rigidbody2D>().gravityScale/10);
            
        }
    }
}
