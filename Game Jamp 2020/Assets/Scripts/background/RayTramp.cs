using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTramp : MonoBehaviour
{
    // Start is called before the first frame update

    private Player player;
    public int damage;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>(); ;

    }

    // Update is called once per frame
    void Update()
    {
        checkNearPlayer();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && !player.isDashing())
        {
            Debug.Log("Activada trampa lazer");
            player.killPlayer();
        }
    }*/


    private void checkNearPlayer()
    {
        Vector3 playerPosition = player.getPosition();
        Vector3 myPosition = this.transform.position;

        double range = 3.5;
        Debug.Log(Vector3.Distance(myPosition, playerPosition));
        if ((Vector3.Distance(myPosition, playerPosition) < range) && !player.isDashing())
        {
            //Do something
            player.killPlayer();
        }
    }
}
