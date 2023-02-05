using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public GameObject rootSegment;
    public GameObject rootEmpty;

    public GameObject bullet;
    public float bulletSpeed = 1.4f;

    private Vector3 direction = new Vector3();
    private Vector3 rotation = new Vector3();

    private float topHeight = -0.5f;
    private float speed = 0.1f;
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
        bool atTop = transform.position.y >= topHeight;
        if (atTop)
            rootEmpty.transform.localScale = Vector3.zero;
        else
            rootEmpty.transform.localScale = Vector3.one;

        float horizontalInput = atTop ? 0 : Input.GetAxisRaw("Horizontal");
        float verticalInput = 0.0f;

        if (horizontalInput == 0.0f)
        {
            verticalInput = atTop ? (Input.GetAxisRaw("Vertical") == -1 ? -1 : 0) : Input.GetAxisRaw("Vertical");
        }

        direction = new Vector3(horizontalInput, verticalInput);
        rotation = new Vector3(0, 0, 90.0f * horizontalInput + (verticalInput != 1.0f ? 180.0f : 0.0f));

        if (atTop && Input.GetButtonDown("Fire1")) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDirection = new Vector2(mouseWorldPos.x, mouseWorldPos.y).normalized;

            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            Debug.Log(angle - 90);


            if (mouseDirection.y > 0.0f)
            {
                Vector3 projRotation = (mouseWorldPos - this.transform.position).normalized;
                GameObject newBullet = Instantiate(bullet, mouseDirection, Quaternion.Euler(0.0f, 0.0f, angle - 90));

                newBullet.GetComponent<Rigidbody2D>().AddForce(mouseDirection * bulletSpeed);
            }
        }
    }

    void FixedUpdate()
    {
        if (direction != Vector3.zero) {
            transform.position = transform.position + direction * speed;
            transform.eulerAngles = rotation;
        }

        Vector2Int position2DBox = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        Vector3 positionCopy = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0.0f);

        if (moves.Count >= 1)
        {
            if (moves.Count >= 2) {
                Vector2Int currentMove = moves.Pop();

                if (moves.Peek() == position2DBox) {
                    moves.Pop();
                    Destroy(instantiatedRootSegments.Pop());
                    Destroy(instantiatedRootSegments.Pop());
                } else if (currentMove == position2DBox) {
                    Destroy(instantiatedRootSegments.Pop());
                } else {
                    moves.Push(currentMove);
                }

                moves.Push(position2DBox);
                GameObject instantiatedObject = Instantiate(rootSegment, positionCopy, Quaternion.identity);
                instantiatedObject.transform.eulerAngles = rotation;
                instantiatedObject.transform.parent = rootEmpty.transform;
                instantiatedRootSegments.Push(instantiatedObject);
            } else if (moves.Peek() != position2DBox) {
                moves.Push(position2DBox);
                GameObject instantiatedObject = Instantiate(rootSegment, positionCopy, Quaternion.identity);
                instantiatedObject.transform.eulerAngles = rotation;
                instantiatedObject.transform.parent = rootEmpty.transform;
                instantiatedRootSegments.Push(instantiatedObject);
            }

        } else {
            moves.Push(position2DBox);
            GameObject instantiatedObject = Instantiate(rootSegment, positionCopy, Quaternion.identity);
            instantiatedObject.transform.eulerAngles = rotation;
            instantiatedObject.transform.parent = rootEmpty.transform;
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

    public void upgrade() {
        speed += 0.05f;
    }

    public override string ToString()
    {
        return "Speed: " + speed;
    }
}
