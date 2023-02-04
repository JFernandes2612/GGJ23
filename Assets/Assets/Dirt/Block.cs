using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(durability == 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            if (durability > 0)
                durability--;
    }
}
