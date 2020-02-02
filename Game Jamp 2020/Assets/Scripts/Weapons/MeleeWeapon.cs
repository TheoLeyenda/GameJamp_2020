using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponsPlayer
{
    // Start is called before the first frame update
    public float SizeWeapon;
    public BoxCollider2D boxCollider2D;
    public float delayActivateBoxCollider2D;
    public float auxDelayActivateBoxCollider2D;
    void Start()
    {
        boxCollider2D.size = new Vector2(SizeWeapon,boxCollider2D.size.y);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        delayActivateBoxCollider2D = auxDelayActivateBoxCollider2D;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            CheckAttack();
        }
    }
    public void CheckAttack()
    {
        if (delayActivateBoxCollider2D > 0)
        { 
            delayActivateBoxCollider2D = delayActivateBoxCollider2D - Time.deltaTime;
        }
        else
        {
            boxCollider2D.gameObject.SetActive(false);
            CountUse--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy e = collision.GetComponent<Enemy>();
            e.life = e.life - Damage;
            delayActivateBoxCollider2D = 0.05f;
        }
    }
}
