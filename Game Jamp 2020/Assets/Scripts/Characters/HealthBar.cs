using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLife(float life)
    {
        float adjustedLife = life / 100;
        bar.localScale = new Vector3(adjustedLife, 1f);
    }
}
