using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    static private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int getDamage() {
        return damage;
    }

    public static void upgrade() {
        damage++;
    }

    void FixedUpdate() {
        Renderer renderer = GetComponent<Renderer>();

        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }

}
