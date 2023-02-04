using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLightController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    private float baseY;
    private float baseYScale;

    void Start()
    {
        baseY = transform.position.y;
        baseYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0.0f, baseY + player.transform.position.y, transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x, baseYScale - player.transform.position.y,transform.localScale.z);
    }
}
