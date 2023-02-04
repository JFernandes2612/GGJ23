using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{

    public GameObject rootSegment;

    private Vector3 prevPosition = new Vector3();
    private Vector3 direction = new Vector3();
    private Vector3 rotation = new Vector3();

    private float blockWidth = 0.1f;
    private Rigidbody2D rb;
    public float knockbackStrength;
    public float knockbackDuration;

    private Stack<Vector2Int> moves = new Stack<Vector2Int>();
    private Stack<GameObject> instantiatedRootSegments = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = 0.0f;

        if (horizontalInput == 0.0f)
        {
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        direction = new Vector3(horizontalInput, verticalInput);
        rotation = new Vector3(0, 0, 90.0f * horizontalInput + (verticalInput != 1.0f ? 180.0f : 0.0f));
    }

    void FixedUpdate()
    {
        prevPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        transform.position = transform.position + direction * blockWidth;
        transform.eulerAngles = rotation;

        Vector2Int prevPosition2DBox = new Vector2Int((int)prevPosition.x, (int)prevPosition.y);
        Vector2Int position2DBox = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        Vector3 positionCopy = new Vector3((int)transform.position.x, (int)transform.position.y, 0.0f);
        Quaternion rotationCopy = Quaternion.Euler(transform.eulerAngles);

        if (prevPosition2DBox != position2DBox)
        {
            if (moves.Count > 1)
            {
                Vector2Int holdValue = moves.Pop();

                if (moves.Peek() == position2DBox)
                {
                    moves.Pop();
                    Destroy(instantiatedRootSegments.Pop());
                    Destroy(instantiatedRootSegments.Pop());
                }
                else
                {
                    moves.Push(holdValue);
                }
            }
            moves.Push(position2DBox);
            GameObject instantiatedObject = Instantiate(rootSegment, positionCopy, rotationCopy);
            instantiatedRootSegments.Push(instantiatedObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
            StartCoroutine(ApplyKnockback());
    }

    IEnumerator ApplyKnockback()
    {
        Vector3 knockbackForce = direction * -1 * knockbackStrength;
        rb.AddForce(knockbackForce);
        yield return new WaitForSeconds(knockbackDuration);
        rb.AddForce(-knockbackForce); //nullifies knockback
    }
}
