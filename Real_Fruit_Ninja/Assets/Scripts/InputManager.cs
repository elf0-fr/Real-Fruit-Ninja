using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent oignonInputEvent;
    [SerializeField]
    private FruitType_gameEvent fruitInputEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pomme
        if (Input.GetKeyDown(KeyCode.E))
        {
            fruitInputEvent.Raise(FruitType.APPLE);
        }
        //Banane
        if (Input.GetKeyDown(KeyCode.R))
        {
            fruitInputEvent.Raise(FruitType.BANANA);
        }
        //Carrot
        if (Input.GetKeyDown(KeyCode.T))
        {
            fruitInputEvent.Raise(FruitType.CARROT);
        }
        //Poire
        if (Input.GetKeyDown(KeyCode.T))
        {
            fruitInputEvent.Raise(FruitType.PEAR);
        }
        //Tomate
        if (Input.GetKeyDown(KeyCode.I))
        {
            fruitInputEvent.Raise(FruitType.TOMATO);
        }
        //Oignon
        if (Input.GetKeyDown(KeyCode.P))
        {
            oignonInputEvent.Raise();
        }
    }
}
