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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Activada trampa lazer");
            player.killPlayer();
        }
    }

    private void OnTriggerExit2d(Collision2D collision)
    {
        Debug.Log("Salio");
        player.transform.parent = null;
    }
}
