using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{

    public GameObject rootSegment;

    private Vector3 direction = new Vector3();
    private Vector3 rotation = new Vector3();
    private float blockWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

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

        direction = new Vector3(horizontalInput, verticalInput, 0);
        rotation = new Vector3(0, 0, 90.0f * horizontalInput + (verticalInput != 1.0f ? 180.0f : 0.0f));
    }

    void FixedUpdate()
    {
        transform.position = transform.position + direction * blockWidth;

        transform.eulerAngles = rotation;
    }
}