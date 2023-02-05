using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject warningLight;

    public float waveDelay = 90.0f;

    public float baseSpawnDelay = 5.0f;

    public float initialDelay = 2.0f;

    public int baseNumberOfEnemies = 5;

    public static int wave = 1;

    private float warningLightFrameRate = 1000.0f;
    private float warningLightAnimationSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator WarningLight() {
        for (float i = 0; i < initialDelay * warningLightFrameRate; i++) {
            Light2D light = warningLight.GetComponent<Light2D>();
            light.intensity = (Mathf.Sin(i * Mathf.PI / (initialDelay * warningLightFrameRate) * warningLightAnimationSpeed - Mathf.PI / 3) + 1) / 2;
            yield return new WaitForSeconds(1.0f / warningLightFrameRate);
        }
    }

    IEnumerator Spawn() {
        while (true) {
            yield return StartCoroutine(WarningLight());

            for (int i = 0; i < baseNumberOfEnemies * wave; i++) {
                GameObject newEnemy = Instantiate(enemy, transform.position + Vector3.left * Random.Range(-10, 11), Quaternion.identity);
                newEnemy.GetComponent<Enemy>().addStats(1 + (wave - 1) * 0.05f, 1 + (wave - 1) * 0.1f);
                yield return new WaitForSeconds(baseSpawnDelay / wave);
            }

            wave++;
            yield return new WaitForSeconds(waveDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
