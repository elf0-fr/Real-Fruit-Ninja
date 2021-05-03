using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent oignonInputEvent;

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

        }
        //Banane
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        //Poire
        if (Input.GetKeyDown(KeyCode.U))
        {

        }
        //Tomate
        if (Input.GetKeyDown(KeyCode.I))
        {

        }
        //Oignon
        if (Input.GetKeyDown(KeyCode.P))
        {
            oignonInputEvent.Raise();
        }
        //carotte ?

    }
}
