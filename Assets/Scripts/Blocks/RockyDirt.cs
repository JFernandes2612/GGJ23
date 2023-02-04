using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RockyDirt : Block
{
    void Start()
    {
        durability = 10;
        lootTable = new Dictionary<string, float>();

        lootTable.Add("C", 0.6f);   // 60%
        lootTable.Add("Si", 0.9f);  // 30%
        lootTable.Add("Fe", 1.0f);  // 10%

        lootAmount = 5;
    }
}
