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
    [SerializeField]
    private GameObject gameOverImage;
    [SerializeField]
    private Text scoreAndLifeText;

    // Start is called before the first frame update
    void Start()
    {
        TransitionUI(gameStates_so.GameState);
        UpdateScoreAndLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameStates_so.GameState = GameState.Play;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TransitionToMenu();
        }
    }

    public void TransitionUI(GameState state)
    {
        switch (gameStates_so.GameState)
        {
            case GameState.Start:
            case GameState.Pause:
                TransitionToMenu();
                break;
            case GameState.Credits:
                TransitionToCredits();
                break;
            case GameState.End:
                TransitionToGameOver();
                break;
            case GameState.Play:
                TransitionToGame();
                break;
            default:
                Debug.LogError("GameState not recognize");
                break;
        }
    }

    public void FruitInput(FruitType fruitType)
    {
        if (gameStates_so.GameState != GameState.Start && gameStates_so.GameState != GameState.Pause)
            return;

        switch (fruitType)
        {
            case FruitType.APPLE:
                gameStates_so.GameState = GameState.Play;
                break;
            case FruitType.BANANA:
                gameStates_so.GameState = GameState.Credits;
                break;
            case FruitType.PEAR:
                gameStates_so.GameState = GameState.Start;
                break;
            default:
                Debug.Log("Input not recognize");
                break;
        }
    }

    public void OignonInput()
    {
        switch (gameStates_so.GameState)
        {
            case GameState.Play:
                gameStates_so.GameState = GameState.Pause;
                break;
            case GameState.Pause:
                gameStates_so.GameState = GameState.Play;
                break;
            case GameState.Credits:
                gameStates_so.GameState = GameState.Pause;
                break;
            case GameState.End:
                gameStates_so.GameState = GameState.Start;
                gameStates_so.ResetScore();
                //TODO Raise event of restart
                break;
            default:
                Debug.LogError("GameState not recognize");
                break;
        }
    }

    public void UpdateScoreAndLife()
    {
        scoreAndLifeText.text = "Points: " + gameStates_so.Score + "\nLife: " + gameStates_so.Life;
    }

    private void TransitionToGame()
    {
        Time.timeScale = 1f;
        startAndPauseImage.SetActive(false);
        creditsImage.SetActive(false);
        gameOverImage.SetActive(false);
    }

    private void TransitionToMenu()
    {
        Time.timeScale = 0f;
        startAndPauseImage.SetActive(true);
        creditsImage.SetActive(false);
        gameOverImage.SetActive(false);
    }

    private void TransitionToCredits()
    {
        Time.timeScale = 0f;
        creditsImage.SetActive(true);
        startAndPauseImage.SetActive(false);
        gameOverImage.SetActive(false);
    }

    private void TransitionToGameOver()
    {
        Time.timeScale = 0f;
        creditsImage.SetActive(false);
        startAndPauseImage.SetActive(false);
        gameOverImage.SetActive(true);
    }
}
