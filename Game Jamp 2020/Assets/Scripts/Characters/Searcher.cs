using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searcher : Enemy
{
    // Start is called before the first frame update
    [Header("Sarcher Parametros")]
    //public float speedBullet;
    private bool enableShoot = false;
    public float delayShoot;
    public float auxDelayShoot;
    public GameObject originalPrefabBullet;
    public GameObject generatorBullet;
    public DirectionShoot directionShoot;

    public AudioClip doneDamage;
    public AudioClip reciveDamage;
    private AudioSource source;
    private float volume = 1.0F;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public enum DirectionShoot
    {
        left,
        right,
    }
    protected override void Start()
    {
        base.Start();
        GameObject go = GameObject.Find("Player");
        player = go.GetComponent<Player>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckStateEnemy();
        CheckDead();
        if (stateEnemy != StateEnemy.Stunt)
        {
            if (player != null)
            {
                CheckPlayerInRange();
                CheckShoot();
            }
            else
            {
                Debug.Log(player);
            }
        }
        else
        {
            //ANIMACION STUNE
            animator.Play("Stune");
            animator.SetBool("Idle", false);
        }
    }
    public void CheckPlayerInRange()
    {
        if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceAttack /*&& Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY*/)
        {
            if (player.transform.position.x < transform.position.x)
            {
                directionShoot = DirectionShoot.left;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (player.transform.position.x > transform.position.x)
            {
                directionShoot = DirectionShoot.right;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            enableShoot = true;
        }
        else
        {
            Movement();
            enableShoot = false;
        }
    }
    public void CheckShoot()
    {
        if (enableShoot)
        {
            if (delayShoot > 0)
            {
                delayShoot = delayShoot - Time.deltaTime;
            }
            else if (delayShoot <= 0)
            {
                delayShoot = auxDelayShoot;
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(originalPrefabBullet, generatorBullet.transform.position, generatorBullet.transform.rotation, null);
        bullet.GetComponent<Bullets>().tagShooter = "Enemy";
        source.PlayOneShot(doneDamage, volume);
    }
}
