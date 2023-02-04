using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Block
{
    void Start()
    {
        durability = 4;
        lootTable = new Dictionary<string, float>();

        lootTable.Add("C", 0.40f); // 40%
        lootTable.Add("O", 0.60f); // 20%
        lootTable.Add("H", 0.70f); // 10%
        lootTable.Add("N", 1.0f);  // 30%

        lootAmount = 3;
    }
}
