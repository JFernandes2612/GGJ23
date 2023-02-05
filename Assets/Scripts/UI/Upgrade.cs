using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public GameObject inventoryObject;
    private Inventory inventory;

    public GameObject costTextObject;
    private TextMeshProUGUI costText;

    private List<string> items = new List<string>(){"C", "O", "H", "N", "Si", "Fe", "Au", "Pt", "U"};
    private List<float> rates = new List<float>(){10.0f, 5.0f, 2.5f, 0.5f, 0.25f, 0.2f, 0.1f, 0.01f, 0.005f};

    private List<int> quantities;

    private int timesUpgraded = 0;

    // Start is called before the first frame update
    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();
        costText = costTextObject.GetComponent<TextMeshProUGUI>();
        updateText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void updateText() {
        costText.text = "";
        quantities = new List<int>();

        for(int i = 0; i < rates.Count; i++) {
            quantities.Add((int)(rates[i] * (timesUpgraded + 1)));
        }

        for (int i = 0; i < quantities.Count && quantities[i] > 0; i++) {
            costText.text += items[i] + " - " + quantities[i] + "\n";
        }
    }

    private bool updateInventory() {
        for (int i = 0; i < quantities.Count; i++) {
            if (!inventory.canRemoveItem(items[i], quantities[i])) {
                return false;
            }
        }

        for (int i = 0; i < quantities.Count; i++) {
            if (!inventory.removeItem(items[i], quantities[i])) {
                return false;
            }
        }

        return true;
    }

    private void upgraded() {
        timesUpgraded++;
        updateText();
    }

    public void upgradeSpeed() {
        if (updateInventory()) {
            TipController.upgrade();
            upgraded();
        }
    }

    public void upgradeDamage() {
        if (updateInventory()) {
            Bullet.upgrade();
            upgraded();
        }
    }

    public void upgradeMining() {
        if (updateInventory()) {
            Block.upgrade();
            upgraded();
        }
    }
}
