using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public Animator animator;
    public void DestroyMine()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.Play("Mine Explosion");
            Player p = collision.GetComponent<Player>();
            p.life = p.life - damage;
        }
    }
}
