using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcoElectrico : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeStunt;
    private float auxTimeStunt;
    public float damage;
    public Animator animator;
    public float delayCharcoNormal;
    public float delayCharcoElectrico;
    private float auxDelayCharcoNormal;
    private float auxDelayCharcoElectrico;
    private bool inStuned;
    public bool damageEnemy = true;
    private StateCharco stateCharco;
    enum StateCharco
    {
        normal,
        electrificado,
    }
    void Start()
    {
        auxDelayCharcoElectrico = delayCharcoElectrico;
        auxDelayCharcoNormal = delayCharcoNormal;
        delayCharcoElectrico = 0;
        auxTimeStunt = timeStunt;
        inStuned = false;
    }

    public void CheckAnimation()
    {
        if (!inStuned)
        {
            timeStunt = auxTimeStunt;
            if (delayCharcoNormal > 0)
            {
                delayCharcoNormal = delayCharcoNormal - Time.deltaTime;
                animator.SetBool("CharcoNormal", true);
                animator.SetBool("CharcoElectrico", false);
            }
            else if (delayCharcoNormal <= 0 && stateCharco == StateCharco.normal)
            {
                delayCharcoElectrico = auxDelayCharcoElectrico;
                stateCharco = StateCharco.electrificado;
            }
            else if (delayCharcoElectrico > 0)
            {
                delayCharcoElectrico = delayCharcoElectrico - Time.deltaTime;
                animator.SetBool("CharcoNormal", false);
                animator.SetBool("CharcoElectrico", true);
            }
            else if (delayCharcoElectrico <= 0 && stateCharco == StateCharco.electrificado)
            {
                delayCharcoNormal = auxDelayCharcoNormal;
                stateCharco = StateCharco.normal;
            }
        }
        else
        {
            if (timeStunt > 0)
            {
                delayCharcoElectrico = 0;
                delayCharcoNormal = auxDelayCharcoNormal;
                animator.SetBool("CharcoNormal", false);
                animator.SetBool("CharcoElectrico", true);
                timeStunt = timeStunt - Time.deltaTime;
            }
            else
            {
                inStuned = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player p = collision.GetComponent<Player>();
            p.timeDelayStune = timeStunt;
            p.statePlayer = Player.StatePlayer.Stunt;
            p.life = p.life - damage;
            inStuned = true;
            p.enableEventStunt = false;
        }
        if (damageEnemy)
        {
            if (collision.tag == "Enemy")
            {
                Enemy e = collision.GetComponent<Enemy>();
                
            }
        }
    }
    /*public void Stunt()
    {
        if (!player.isDashing())
        {
            player.timeDelayStune = delayAttack + 1;
            player.statePlayer = Player.StatePlayer.Stunt;
            fsm.SendEvent((int)Events.ReadyToAttack);
            player.life = player.life - Damage;
            playerScape = false;
        }

    }*/
}
