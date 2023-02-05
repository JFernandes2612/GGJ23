using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upgradeSpeed() {
        TipController.upgrade();
    }

    public void upgradeDamage() {
        Bullet.upgrade();
    }

    public void upgradeMining() {
        Block.upgrade();
    }
}
