using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeItem
    {
        Default,
        BrazoDeShoker,
        RifleTracker,
        SableAssasin,
    }
    public float lifeRecover;
    public TypeItem typeItem;
}
