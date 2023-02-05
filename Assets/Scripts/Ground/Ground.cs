using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    float groundBaseHeight = 0f;
    int groundWidth = 23;
    int groundHeight = 1000;
    GameObject[,] blocks;
    [SerializeField]
    private GameObject surfacePrefab;
    [SerializeField]
    private GameObject bedrockPrefab;
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
            float heightRate = (float)(i) / (float)groundHeight;
            List<float> prob = new List<float>(3) {1.0f-heightRate,1.0f-heightRate + heightRate*2/3, 1.0f};
            for (int j = 0; j < groundWidth; j++)
            {
                GameObject blockPrefab = blockPrefabs[0];
                if (x == 0 || x == groundWidth - 1 || y == groundHeight - 1) {
                    blockPrefab = bedrockPrefab;
                } else if (y == groundBaseHeight) {
                    blockPrefab = surfacePrefab;
                } else {
                    for (int c = 0; c < prob.Count; c++) {
                        if (Random.Range(0.0f, 1.0f) < prob[c]) {
                            blockPrefab = blockPrefabs[c];
                            break;
                        }
                    }
                }
                Vector3 blockPos = transform.position + new Vector3(x, y);
                GameObject newBlock = Instantiate(blockPrefab, blockPos, Quaternion.identity);
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
