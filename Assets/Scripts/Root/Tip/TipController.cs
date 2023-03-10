using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public GameObject rootSegment;
    public GameObject rootEmpty;

    public GameObject bulletsEmpty;
    public GameObject bullet;
    public float bulletSpeed = 1.4f;

    private Vector3 direction = new Vector3();
    private Vector3 rotation = new Vector3();

    private float topHeight = -0.5f;
    private static float speed = 0.1f;
    private Rigidbody2D rb;

    private Inventory inventory;

    private Stack<Vector2Int> moves = new Stack<Vector2Int>();
    private Stack<GameObject> instantiatedRootSegments = new Stack<GameObject>();

    private bool canCollide = true;
    public static float collisionCooldown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
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

            if (mouseDirection.y > 0.0f)
            {
                Vector3 projRotation = (mouseWorldPos - this.transform.position).normalized;
                GameObject newBullet = Instantiate(bullet, mouseDirection, Quaternion.Euler(0.0f, 0.0f, angle - 90));
                newBullet.transform.parent = bulletsEmpty.transform;
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
                if (moves.Count == 1) {
                    instantiatedObject.SetActive(false);
                }
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
            instantiatedObject.SetActive(false);
            instantiatedObject.transform.eulerAngles = rotation;
            instantiatedObject.transform.parent = rootEmpty.transform;
            instantiatedRootSegments.Push(instantiatedObject);
        }
    }

    public static void upgradeSpeed() {
        speed += 0.005f;
    }

    public static void upgradeCollisionCooldown()
    {
        collisionCooldown -= 0.005f;
    }

    public override string ToString()
    {
        return "Speed: " + (speed*100).ToString("#.#") + "\nMining Speed: " + (1 / collisionCooldown).ToString("#.##");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            if (canCollide)
            {
                canCollide = false;
                Vector2 collisionDirection = collision.transform.position - transform.position;
                Block blockScript = collision.gameObject.GetComponent<Block>();
                blockScript.Collide(collisionDirection, inventory);
                StartCoroutine(collideCooldown());
            }
        }
    }

    IEnumerator collideCooldown()
    {
        yield return new WaitForSeconds(collisionCooldown);
        canCollide = true;
    }
}
