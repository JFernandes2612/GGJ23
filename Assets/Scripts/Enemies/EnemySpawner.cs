using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float spawnDelay = 5.0f;
    public float speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (true) {
            Instantiate(enemy, transform.position + Vector3.left * Random.Range(-10, 11) * speed, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
