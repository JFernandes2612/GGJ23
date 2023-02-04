using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected int durability;

    protected Dictionary<string, float> lootTable;
    protected int lootAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(durability == 0) {
            Destroy(gameObject);

            for (int i = 0; i < lootAmount; i++) {
                float drop = Random.Range(0.0f, 1.0f);

                foreach (KeyValuePair<string, float> entry in lootTable)
                {
                    if (drop < entry.Value) {
                        // Change to actual drop behaviour
                        Debug.Log("Dropped item #" + (i+1) + ": " + entry.Key);
                        break;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            if (durability > 0)
                durability--;
    }
}
