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

    }
    // Update is called once per frame
    void Update()
    {
        CheckStateLaser();
    }
}
