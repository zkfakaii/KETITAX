using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptPlague : MonoBehaviour

{
    public GameObject spearPrefab; // Prefab de la lanza
    public Vector2 areaSize = new Vector2(10, 5); // Tamaño del área rectangular (ancho, alto)
    public float spawnIntervalMin = 0.5f; // Intervalo mínimo entre la aparición de lanzas
    public float spawnIntervalMax = 1.5f; // Intervalo máximo entre la aparición de lanzas
    public float spearSpeed = 5f; // Velocidad de caída de las lanzas
    public float spearLifetime = 5f; // Tiempo de vida de las lanzas

    private float timeSinceLastSpawn;

    void Start()
    {
        timeSinceLastSpawn = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update()
    {
        timeSinceLastSpawn -= Time.deltaTime;

        if (timeSinceLastSpawn <= 0)
        {
            SpawnSpear();
            timeSinceLastSpawn = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    void SpawnSpear()
    {
        float xPos = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        Vector3 spawnPosition = new Vector3(transform.position.x + xPos, transform.position.y + areaSize.y / 2, 0);

        GameObject spear = Instantiate(spearPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -spearSpeed);
        }

        Destroy(spear, spearLifetime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 1));
    }
}
