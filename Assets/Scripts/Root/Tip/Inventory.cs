using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void putItem(string s) {
        items[s]++;
    }

    public bool canRemoveItem(string s, int quantity) {
         if (items[s] >= quantity) {
            return true;
        }

        return false;
    }

    public bool removeItem(string s, int quantity) {
        if (canRemoveItem(s, quantity)) {
            items[s] -= quantity;
            return true;
        }

        return false;
    }

    override public string ToString() {
        string s = "";

        foreach (KeyValuePair<string, int> entry in items) {
            s += entry.Key + " - " + entry.Value + "    ";
        }

        return s;
    }

    // Start is called before the first frame update
    void Start()
    {
        items.Add("C", 0);
        items.Add("O", 0);
        items.Add("H", 0);
        items.Add("N", 0);
        items.Add("Si", 0);
        items.Add("Fe", 0);
        items.Add("Au", 0);
        items.Add("Pt", 0);
        items.Add("U", 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
