using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float minDamage;
    public float Damage;
    public float speedSubstractDamage;
    public float speedSubstractScale;
    public float Scale;
    public float minScale;
    public Vector3 rotation;
    public float timeLife;
    [HideInInspector]
    public bool enableMovement = true;

    private void Start()
    {
        rotation = Vector3.zero;
    }
    private void Update()
    {
        if (enableMovement)
        {

            CheckCharacteristic();
            Movement(transform.right);
            CheckTimeLife();
        }
    }
    public void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if(timeLife <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void CheckCharacteristic()
    {
        if (Damage > minDamage)
        {
            Damage = Damage - Time.deltaTime * speedSubstractDamage;
        }
        else if (Damage <= minDamage)
        {
            Damage = minDamage;
        }
        if (Scale > minScale)
        {
            Scale = Scale - Time.deltaTime * speedSubstractScale;
        }
        else if (Scale <= minScale)
        {
            Scale = minScale;
        }

        transform.localScale = new Vector3(Scale, Scale, 1);
    }
    public void Movement( Vector2 dir)
    {
        transform.position = transform.position + new Vector3(speed * dir.x, 0, 0) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
          ;
            Player player = collision.gameObject.GetComponent<Player>();
            if (!player.isDashing())
            {
                player.life = player.life - Damage;
                timeLife = 0.1f;
            }

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.life = enemy.life - Damage;
            timeLife = 0.1f;
        }
    }
}
