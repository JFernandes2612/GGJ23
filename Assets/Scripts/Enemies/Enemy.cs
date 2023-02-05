using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float baseSpeed = 0.05f;

    private int baseHealth = 5;

    private float speed;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position += -transform.position.normalized * speed;
    }

    public void addStats(float speedFactor, float healthFactor) {
        speed = baseSpeed * speedFactor;
        health = (int)(baseHealth * healthFactor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
            Destroy(gameObject);
            // END GAME OR SOMETHING

        if (collision.gameObject.tag == "Bullet") {
            if (health > 0) {
                health -= Bullet.getDamage();
            } else if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
