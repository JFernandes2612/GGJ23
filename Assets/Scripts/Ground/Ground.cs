using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    int groundWidth = 25;
    int groundHeight= 25;
    GameObject[,] blocks;
    public GameObject[] blockPrefabs;
    int blockOffset = 1;

    // Start is called before the first frame update
    void Start()
    {
        blocks = new GameObject[groundHeight, groundWidth];
        float y = 0f;
        for (int i = 0; i < groundHeight; i++)
        {
            float x = 0f;
            for (int j = 0; j < groundWidth; j++)
            {
                int selectedPrefabID = Random.Range(0, blockPrefabs.Length - 1);
                Vector3 blockPos = new Vector3(x, y);
                GameObject newBlock = Instantiate(blockPrefabs[selectedPrefabID], blockPos, Quaternion.Euler(new Vector3(0,0,0)));
                blocks[i, j] = newBlock;
                x += blockOffset;
            }
            y -= blockOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
