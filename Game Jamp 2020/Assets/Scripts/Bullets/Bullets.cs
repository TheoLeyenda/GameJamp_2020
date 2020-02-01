using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float minDamage;
    public float Damage;
    public float Scale;
    public float minScale;

    public void CheckCharacteristic()
    {
        if (Damage > minDamage)
        {
            Damage = Damage - Time.deltaTime;
        }
        else if (Damage <= minDamage)
        {
            Damage = minDamage;
        }
        if (Scale > minScale)
        {
            Scale = Scale - Time.deltaTime;
        }
        else if (Scale <= minScale)
        {
            Scale = minScale;
        }

        transform.localScale = new Vector3(Scale, Scale, Scale);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.life = player.life - Damage;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.life = enemy.life - Damage;
        }
    }
}
