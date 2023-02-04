using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Block
{
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        durabilityFactor = 4;
        durability = baseDurability * durabilityFactor;
        lootTable = new Dictionary<string, float>();

        lootTable.Add("C", 0.4f);   // 40%
        lootTable.Add("Si", 0.65f); // 25%
        lootTable.Add("Fe", 0.8f);  // 15%
        lootTable.Add("Au", 0.9f);  // 10%
        lootTable.Add("Pt", 0.95f); // 5%
        lootTable.Add("U", 1.0f);   // 5%

        lootAmount = 3;
    }
}
