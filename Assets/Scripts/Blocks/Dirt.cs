using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Block
{

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        durabilityFactor = 1;
        durability = baseDurability * durabilityFactor;
        lootTable = new Dictionary<string, float>();

        lootTable.Add("C", 0.40f); // 40%
        lootTable.Add("O", 0.60f); // 20%
        lootTable.Add("H", 0.70f); // 10%
        lootTable.Add("N", 1.0f);  // 30%

        lootAmount = 6;
    }
}
