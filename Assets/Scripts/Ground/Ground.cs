using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    float groundBaseHeight = 0f;
    int groundWidth = 23;
    int groundHeight = 10;
    GameObject[,] blocks;
    [SerializeField]
    private GameObject surfacePrefab;
    [SerializeField]
    private GameObject[] blockPrefabs;
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
                GameObject blockPrefab = (y == groundBaseHeight) ? (surfacePrefab) : blockPrefabs[Random.Range(0, blockPrefabs.Length)];
                Vector3 blockPos = transform.position + new Vector3(x, y);
                GameObject newBlock = Instantiate(blockPrefab, blockPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                newBlock.transform.parent = gameObject.transform;
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
