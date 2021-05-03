using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameStates_so gameStates_so;

    [SerializeField]
    private GameObject startAndPauseImage;
    [SerializeField]
    private GameObject creditsImage;

    private void Awake()
    {
        this.startAndPauseImage.SetActive(true);
        this.creditsImage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TransitionToGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TransitionToMenu();
        }
    }

    public void TransitionToGame()
    {
        gameStates_so.GameState = GameState.Play;
        startAndPauseImage.SetActive(false);
    }

    public void TransitionToMenu()
    {
        gameStates_so.GameState = GameState.Pause;
        startAndPauseImage.SetActive(true);
        creditsImage.SetActive(false);
    }

    public void TransitionToCredits()
    {
        gameStates_so.GameState = GameState.Credits;
        creditsImage.SetActive(true);
        startAndPauseImage.SetActive(false);
    }
}
