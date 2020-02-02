using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour { 

    private Player player;
    private GameObject cameraVision;
    private int counter;
    private bool cameraActive;
    public int intermitentTime;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        cameraVision = GameObject.FindWithTag("CameraVision");
        counter = 0;
        cameraActive = true;
    }

    // Update is called once per frame
    void Update()
    {
       this.intermitentEffect();

    }

    private void intermitentEffect()
    {
        Debug.Log(this.counter);
        if (this.counter > this.intermitentTime)
        {
            Debug.Log("Entro");
            this.counter = 0;
            this.cameraActive = !this.cameraActive; 
            cameraVision.SetActive(this.cameraActive);
        }
        this.counter++;
    }
}
