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

    // Start is called before the first frame update
    void Start()
    {
        InitializeFruit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeFruit()
    {
        Vector2 spawn = new Vector2(Random.Range(-maxDistance, maxDistance), -5f);

        GameObject fruit = Instantiate(fruitPrefab, spawn, Quaternion.identity);

        fruit.GetComponent<FruitController>().JumpForce.x = Random.Range(-maxAngle, maxAngle);

        //TODO Ajouter les sprites
    }

    // Fruit cut
    public void IncreaseScore()
    {
        score += 10;
    }

    // Wrong fruit cut
    public void DecreaseScore()
    {
        score -= 3;
    }

    // Fruit miss
    public void DecreaseLife()
    {
        life -= 1;
    }
}
