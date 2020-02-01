using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public Characther characther;
    public void DamageCharacter()
    {
        characther.life = characther.life - damage;
    }
}
