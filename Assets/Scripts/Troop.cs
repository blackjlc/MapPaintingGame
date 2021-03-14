using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop
{
    public Country.Name country;
    // 1 for 1k
    public float count;

    public Troop(Country.Name country, float count)
    {
        this.country = country;
        this.count = count;
    }
}
