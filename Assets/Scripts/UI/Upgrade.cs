using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject inventoryObject;

    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upgradeSpeed() {
        TipController.upgradeSpeed();
    }

    public void upgradeDamage() {
        Bullet.upgrade();
    }

    public void upgradeMining() {
        TipController.upgradeCollisionCooldown();
    }
}
