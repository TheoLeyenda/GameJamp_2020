using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWeapon : WeaponsPlayer
{
    // Start is called before the first frame update
    //public float speedBullet;
    private bool enableShoot = false;
    public float delayShoot;
    public float auxDelayShoot;
    public GameObject originalPrefabBullet;
    public GameObject generatorBullet;
    public DistanceWeapon dataDistanceWeapon;
    public bool iAmData;
    void Start()
    {
        if (dataDistanceWeapon != null)
        {
            Damage = dataDistanceWeapon.Damage;
            CountUse = dataDistanceWeapon.CountUse;
            auxCountUse = dataDistanceWeapon.auxCountUse;
            delayShoot = dataDistanceWeapon.delayShoot;
            auxDelayShoot = dataDistanceWeapon.auxDelayShoot;
            originalPrefabBullet = dataDistanceWeapon.originalPrefabBullet;
            generatorBullet = dataDistanceWeapon.generatorBullet;
        }
    }
    private void OnDisable()
    {
        CountUse = auxCountUse;
        delayShoot = 0;
    }
    // Update is called once per frame
    void Update()
    {
       /*if (!iAmData)
        {
            if (gameObject.activeSelf)
            {
                CheckDelayShoot();
            }
        }
        else */if (iAmData)
        {
            CheckBullets();
        }
    }
    public void CheckBullets()
    {
        if (CountUse <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void CheckDelayShoot()
    {
        if (delayShoot > 0)
        {
            delayShoot = delayShoot - Time.deltaTime;
        }
        else if(delayShoot <= 0)
        {
            delayShoot = auxDelayShoot;
            ShootWeapon();
        }
    }
    public void ShootWeapon()
    {
        GameObject bullet = Instantiate(originalPrefabBullet, generatorBullet.transform.position, generatorBullet.transform.rotation, null);
        bullet.GetComponent<Bullets>().tagShooter = "Player";
        CountUse--;
    }
}
