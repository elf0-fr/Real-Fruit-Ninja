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
    private float initialSpawnRate;
    [SerializeField]
    private float initialSpawnRateDropSpeed = 0.001f;
    [SerializeField]
    private float initialSpawnRateDeceleration = 0.001f;
    [SerializeField]
    private float minSpawnRate = 1f;
    private float spawnRate;
    private float spawnRateDropSpeed;
    private float spawnRateDeceleration;
    private float lastSpawnTime;
    private float accumulatedTime;

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] appleList;
    [SerializeField]
    private Sprite[] bananaList;
    [SerializeField]
    private Sprite[] carrotList;
    [SerializeField]
    private Sprite[] pearList;
    [SerializeField]
    private Sprite[] tomatoList;
    private Sprite[][] fruitSpritesArray = new Sprite[5][];

    private readonly List<GameObject> waitingFruits = new List<GameObject>();
    private readonly List<GameObject> inGameFruits = new List<GameObject>();

    private void Awake()
    {
        fruitSpritesArray[(int)FruitType.BANANA] = (bananaList);
        fruitSpritesArray[(int)FruitType.APPLE] = (appleList);
        fruitSpritesArray[(int)FruitType.CARROT] = (carrotList);
        fruitSpritesArray[(int)FruitType.PEAR] = (pearList);
        fruitSpritesArray[(int)FruitType.TOMATO] = (tomatoList);

        Restart();
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
        CheckTimeToSpawn();
    }

    public void Restart()
    {
        lastSpawnTime = 0f;
        accumulatedTime = 0f;

        spawnRate = initialSpawnRate;
        spawnRateDropSpeed = initialSpawnRateDropSpeed;
        spawnRateDeceleration = initialSpawnRateDeceleration;
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

        FruitType randomFruitType = (FruitType)Random.Range(0, ((int) FruitType.FRUITS));
        Sprite[] randomFruitSprites = fruitSpritesArray[(int)randomFruitType];

        fruit.GetComponent<FruitController>().sR_fruit.sprite = randomFruitSprites[0];
        fruit.GetComponent<FruitController>().sR_fruitA.sprite = randomFruitSprites[2];
        fruit.GetComponent<FruitController>().sR_fruitB.sprite = randomFruitSprites[1];
        fruit.GetComponent<FruitController>().fruitType = randomFruitType;

        fruit.SetActive(true);
        fruit.GetComponent<FruitController>().IsWaiting = false;
    }

    public void Despawn(GameObject gameObject)
    {
        inGameFruits.Remove(gameObject);
        waitingFruits.Add(gameObject);
    }

    public void CutFruit(FruitType fruitType)
    {
        if (gameStates_So.GameState != GameState.Play)
            return;

        for (int i = 0; i < inGameFruits.Count; i++)
        {
            if (inGameFruits[i].GetComponent<FruitController>().fruitType == fruitType && !inGameFruits[i].GetComponent<FruitController>().IsCut)
            {
                inGameFruits[i].GetComponent<FruitController>().Cut();
                return;
            }
        }
        gameStates_So.DecreaseScore();
    }
}
