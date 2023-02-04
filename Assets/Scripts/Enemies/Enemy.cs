using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.05f;

    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position += -transform.position.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Player")
            Destroy(gameObject);
            // END GAME OR SOMETHING

        if (collision.gameObject.tag == "Bullet") {
            if (health > 0) {
                health--;
            } else if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
