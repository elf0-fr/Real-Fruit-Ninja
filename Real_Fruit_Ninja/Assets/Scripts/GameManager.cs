using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    private int life = 5;

    [SerializeField]
    private GameObject fruitPrefab;

    [Header("SpawnSettings")]
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float maxAngle;

    [SerializeField]
    private float spawnRate = 10f;
    public float spawnRateDropSpeed = 0.001f;
    public float spawnRateDeceleration = 0.001f;
    [SerializeField]
    private float minSpawnRate = 1f;

    private float lastSpawnTime;
    private float accumulatedTime;

    private List<GameObject> waitingFruits = new List<GameObject>();
    private List<GameObject> inGameFruits = new List<GameObject>();

    private void Awake()
    {
        lastSpawnTime = 0f;
        accumulatedTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            InitializeFruit();
        }

        Debug.Log("Score = " + score);
        Debug.Log("Life = " + life);
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        spawnRateDropSpeed += spawnRateDeceleration * Time.deltaTime;
        spawnRate = Mathf.Max(spawnRate - Time.deltaTime * spawnRateDropSpeed, minSpawnRate);

        if (accumulatedTime - lastSpawnTime > spawnRate)
        {
            lastSpawnTime = accumulatedTime;
            Spawn();
        }
    }

    private void InitializeFruit()
    {
        Vector2 spawn = new Vector2(Random.Range(-maxDistance, maxDistance), -5f);

        GameObject fruit = Instantiate(fruitPrefab, spawn, Quaternion.identity);

        fruit.GetComponent<FruitController>().JumpForce.x = Random.Range(-maxAngle, maxAngle);

        waitingFruits.Add(fruit);
    }

    private void Spawn()
    {
        if (waitingFruits.Count == 0)
        {
            InitializeFruit();
        }

        GameObject fruit = waitingFruits[0];
        waitingFruits.Remove(fruit);
        inGameFruits.Add(fruit);

        //TODO Ajouter les sprites

        fruit.SetActive(true);
        fruit.GetComponent<FruitController>().IsWaiting = false;
    }

    public void Despawn(GameObject gameObject)
    {
        inGameFruits.Remove(gameObject);
        waitingFruits.Add(gameObject);
    }

    // Fruit cut
    public void IncreaseScore()
    {
        score += 10;
        Debug.Log("Score = " + score);
    }

    // Wrong fruit cut
    public void DecreaseScore()
    {
        score -= 3;
        Debug.Log("Score = " + score);
    }

    // Fruit miss
    public void DecreaseLife()
    {
        life -= 1;
        Debug.Log("Life = " + life);
    }
}
