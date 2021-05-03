using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Play,
    Pause,
    Credits,
    End
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameStates_so gameStates_So;

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

    private readonly List<GameObject> waitingFruits = new List<GameObject>();
    private readonly List<GameObject> inGameFruits = new List<GameObject>();

    private void Awake()
    {
        lastSpawnTime = 0f;
        accumulatedTime = 0f;
    }

    void Start()
    {
        for (int i = 0; i < 10; ++i)
        {
            InitializeFruit();
        }

        Debug.Log("Score = " + gameStates_So.Score);
        Debug.Log("Life = " + gameStates_So.Life);
    }

    void Update()
    {
        switch (gameStates_So.GameState)
        {
            case GameState.Start:
                break;
            case GameState.Pause:
            case GameState.Credits:
                break;
            case GameState.End:
                Debug.Log("End");
                break;
            default:
                CheckTimeToSpawn();
                break;
        }
    }

    private void InitializeFruit()
    {
        Vector2 spawn = new Vector2(Random.Range(-maxDistance, maxDistance), -5f);
        GameObject fruit = Instantiate(fruitPrefab, spawn, Quaternion.identity);
        waitingFruits.Add(fruit);
        fruit.SetActive(false);
    }

    private void CheckTimeToSpawn()
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

    private void Spawn()
    {
        if (waitingFruits.Count == 0)
        {
            InitializeFruit();
        }

        GameObject fruit = waitingFruits[0];
        waitingFruits.Remove(fruit);
        inGameFruits.Add(fruit);

        fruit.GetComponent<FruitController>().JumpForce.x = Random.Range(-maxAngle, maxAngle);

        //TODO Ajouter les sprites

        fruit.SetActive(true);
        fruit.GetComponent<FruitController>().IsWaiting = false;
    }

    public void Despawn(GameObject gameObject)
    {
        inGameFruits.Remove(gameObject);
        waitingFruits.Add(gameObject);
    }
}
