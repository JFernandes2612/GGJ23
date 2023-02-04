using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected int durability;
    protected int durabilityFactor;
    protected int baseDurability = 4;

    protected Dictionary<string, float> lootTable;
    protected int lootAmount;

    [SerializeField]
    private Sprite[] damagedSpritesDown, damagedSpritesLeft, damagedSpritesRight, damagedSpritesUp;
    protected SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Sprite[] spritesArray;
            Vector2 collisionDirection = transform.position - collision.transform.position;
            if(Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y)) //hitting horizontally
            {
                spritesArray = (collisionDirection.x < 0) ? damagedSpritesRight : damagedSpritesLeft;
            }
            else //hitting vertically
            {
                spritesArray = (collisionDirection.y < 0) ? damagedSpritesDown : damagedSpritesUp;
            }

            if (durability > 0)
            {
                durability--;
                spriteRenderer.sprite = spritesArray[durability / durabilityFactor];
            } else if (durability <= 0) {
                Destroy(gameObject);

                for (int i = 0; i < lootAmount; i++)
                {
                    float drop = Random.Range(0.0f, 1.0f);

                    foreach (KeyValuePair<string, float> entry in lootTable)
                    {
                        if (drop < entry.Value)
                        {
                            // Change to actual drop behaviour
                            Debug.Log("Dropped item #" + (i + 1) + ": " + entry.Key);
                            break;
                        }
                    }
                }
            }
        }
    }
}
