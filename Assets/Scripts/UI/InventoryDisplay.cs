using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryObject;

    private Inventory inventory;
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        inventory = inventoryObject.GetComponent<Inventory>();
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = inventory.ToString();
    }
}
