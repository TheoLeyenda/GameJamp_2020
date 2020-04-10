using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    public float delayPrendido;
    public float delayApagado;
    public Animator animator;
    private float auxDelayPrendido;
    private float auxDelayApagado;
    private StateLaser stateLaser;
    public bool damageEnemy = true;
    private enum StateLaser
    {
        Off,
        On,
    }
    void Start()
    {
        auxDelayApagado = delayApagado;
        auxDelayPrendido = delayPrendido;
        delayPrendido = 0;
        stateLaser = StateLaser.Off;
    }
    public void CheckStateLaser()
    {
        if(delayApagado > 0 && stateLaser == StateLaser.Off)
        {
            animator.SetBool("Apagado", true);
            animator.SetBool("Prendido", false);
            animator.SetBool("Preparando", false);
            delayApagado = delayApagado - Time.deltaTime;
        }
        else if(delayApagado <= 0 && stateLaser == StateLaser.Off)
        {
            animator.SetBool("Apagado", false);
            animator.SetBool("Prendido", false);
            animator.SetBool("Preparando", true);
        }
        else if(delayPrendido > 0 && stateLaser == StateLaser.On)
        {
            animator.SetBool("Apagado", false);
            animator.SetBool("Prendido", true);
            animator.SetBool("Preparando", false);
            delayPrendido = delayPrendido - Time.deltaTime;
        }
        else if(delayPrendido <= 0 && stateLaser == StateLaser.On)
        {
            animator.SetBool("Apagado", true);
            animator.SetBool("Prendido", false);
            animator.SetBool("Preparando", false);
            stateLaser = StateLaser.Off;
            delayApagado = auxDelayApagado;
        }
    }
    public void ResetDelayPrendido()
    {
        delayPrendido = auxDelayPrendido;
        stateLaser = StateLaser.On;
    }
    // Update is called once per frame
    void Update()
    {
        CheckStateLaser();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageEnemy)
        {
            if (collision.tag == "Enemy")
            {
                Enemy e = collision.GetComponent<Enemy>();
                e.life = 0;
            }
        }
        if(collision.tag == "Player")
        {
            Player p = collision.GetComponent<Player>();
            p.life = 0;
        }
    }
}
