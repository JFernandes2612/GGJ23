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

    public void Collide(Vector2 collisionDirection, Inventory inventory)
    {
        Sprite[] spritesArray;
        if (Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y)) //hitting horizontally
        {
            spritesArray = (collisionDirection.x < 0) ? damagedSpritesLeft : damagedSpritesRight;
        }
        else //hitting vertically
        {
            spritesArray = (collisionDirection.y < 0) ? damagedSpritesDown : damagedSpritesUp;
        }

        if (durability > 0)
        {
            durability--;
            spriteRenderer.sprite = spritesArray[durability / durabilityFactor];
        }
        else if (durability <= 0)
        {
            Destroy(gameObject);

            for (int i = 0; i < lootAmount; i++)
            {
                float drop = Random.Range(0.0f, 1.0f);

                foreach (KeyValuePair<string, float> entry in lootTable)
                {
                    if (drop < entry.Value)
                    {
                        inventory.putItem(entry.Key);
                        break;
                    }
                }
            }
        }
    }
}
