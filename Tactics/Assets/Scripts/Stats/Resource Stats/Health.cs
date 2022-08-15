using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ResourceStat
{   
    private void Start() {
        current = GetComponent<BaseStats>().GetHealth();
    }
}
