using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite tipSprite;
    [SerializeField]
    private Sprite[] segmentSprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        setSpriteTip();
    }

    void setSpriteTip()
    {
        spriteRenderer.sprite = tipSprite;
    }

    void setSpriteSegment()
    {
        spriteRenderer.sprite = segmentSprites[Random.Range(0, segmentSprites.Length)];
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            setSpriteSegment();
        }
    }

}
