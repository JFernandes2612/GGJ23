using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField]
    private GameObject rootTip;
    private Vector3 mousePosition;
    private float topHeight = -0.5f;

    // Update is called once per frame
    void Update()
    {
        if (rootTip.transform.position.y >= topHeight)
        {
            mousePosition = Input.mousePosition;

            float rotation = getCannonAngle();
            transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
        }
    }

    float getCannonAngle()
    {
        Vector2 cannonToMouse = Camera.main.ScreenToWorldPoint(mousePosition) - this.transform.position;
        cannonToMouse.Normalize();

        return Mathf.Atan2(cannonToMouse.y, cannonToMouse.x) * Mathf.Rad2Deg;
    }
}
