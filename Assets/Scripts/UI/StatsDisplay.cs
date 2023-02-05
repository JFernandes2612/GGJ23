using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    public GameObject inventoryObject;

    private TipController player;
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        player = inventoryObject.GetComponent<TipController>();
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = player.ToString();
    }
}
