﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searcher : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedBullet;
    public float delayShoot;
    public float auxDelayShoot;
    public GameObject originalPrefabBullet;
    public GameObject generatorBullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {

        //INSTANCIO PRIMERO Y LUEGO AGREGO LA VELOCIDAD Y TODO LO DEMAS.
        Bullets bullets = originalPrefabBullet.GetComponent<Bullets>();
    }
}