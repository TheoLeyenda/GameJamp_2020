using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Enemy
{
    public enum StateAssasin
    {
        Mine,
        Assasin,
    }
    private bool patrol = false;
    private bool dodgeMe = false;
    public float delayAssesin;
    private bool startDelayMina;
    public float delayActivateMina;
    public float normalAttackDelay;
    public float auxNormalAttackDelay;
    public float distanceMina;
    //public BoxCollider2D boxCollider2D;
    public StateAssasin stateAssasin;
    public Color colorWarning;
    public SpriteRenderer spriteRenderer;
    private bool inTrigger = false;

    private bool inMine = false;

    void Start()
    {
        GameObject go = GameObject.Find("Player");
        player = go.GetComponent<Player>();
        auxSpeedMovement = speedMovement;
        rig2D = GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        CheckDead();
        if (patrol)
        {
            if (enableMovement)
            {
                Movement();
            }
        }
        if (player != null)
        {
            ChasePlayer();
            CheckDodge();
            CheckAttackMine();
            CheckCollider();
        }
    }
    public void ChasePlayer()
    {
        if (stateAssasin == StateAssasin.Assasin)
        {
            if (player.transform.position.x < transform.position.x)
            {
                LeftMovement(false);
            }
            else if (player.transform.position.x > transform.position.x)
            {
                RightMovement(false);
            }

            if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceChase && Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY)
            {
                speedMovement = auxSpeedMovement;
            }
            else
            {
                speedMovement = 0;
                rig2D.velocity = Vector2.zero;
                rig2D.angularVelocity = 0;
            }
        }
    }
    public void CheckCollider()
    {
        if (stateAssasin == StateAssasin.Assasin)
        {
            //boxCollider2D.isTrigger = false;
            if (player.life > 0)
            {
                spriteRenderer.color = Color.white;
            }
        }
        else if (stateAssasin == StateAssasin.Mine)
        {
            //boxCollider2D.isTrigger = true;
        }
    }
    void CheckDodge()
    {
        if (!inTrigger)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position + new Vector3(0, 5, 0), transform.up, 500);
            //Debug.DrawRay(transform.position, transform.up * 500);
            if (raycastHit2D.collider != null)
            {
                if (raycastHit2D.collider.tag == "Player")
                {
                    dodgeMe = true;
                }
            }
            if (dodgeMe)
            {
                if (delayAssesin > 0)
                {
                    delayAssesin = delayAssesin - Time.deltaTime;
                }
                else if (delayAssesin <= 0)
                {
                    stateAssasin = StateAssasin.Assasin;
                }
            }
        }
    }
    public void CheckAttackMine()
    {

        if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceMina
            && Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY)
        {
            if (stateAssasin == StateAssasin.Mine)
            {
                if (delayActivateMina <= 0)
                {
                    spriteRenderer.color = Color.red;
                    player.life = 0;
                }
                else if (delayActivateMina > 0)
                {
                    inMine = true;
                    spriteRenderer.color = colorWarning;
                    delayActivateMina = delayActivateMina - Time.deltaTime;
                }
            }

        }
        else if(inMine)
        {
            if (Mathf.Abs((transform.position.x - player.transform.position.x)) > distanceMina
            || Mathf.Abs((transform.position.y - player.transform.position.y)) > distaceY)
            {
                stateAssasin = StateAssasin.Assasin;
                normalAttackDelay = 0;
            }
        }

        if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceAttack
            && Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY)
        {
            speedMovement = 0;
            if (stateAssasin == StateAssasin.Assasin)
            {
                if (normalAttackDelay <= 0)
                {
                    player.life = player.life - Damage;
                    normalAttackDelay = auxNormalAttackDelay;
                }
                else if (normalAttackDelay > 0)
                {
                    normalAttackDelay = normalAttackDelay - Time.deltaTime;
                }
            }
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                inTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                inTrigger = false;
            }
        }
    }
}
