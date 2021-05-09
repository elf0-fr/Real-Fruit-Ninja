using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameStates", menuName = "CustomSO/GameStates")]
public class GameStates_so : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private GameState_gameEvent gameState_changedEvent;
    [SerializeField]
    private GameEvent scoreOrLife_changedEvent;

    [SerializeField]
    private GameState initialGameState;
    private GameState runtimeGameState;
    public GameState GameState
    {
        get => runtimeGameState;
        set
        {
            runtimeGameState = value;
            gameState_changedEvent.Raise(value);
        }
    }

    [SerializeField]
    private int initialScore;
    private int runtimeScore;
    public int Score
    {
        get => runtimeScore;
        private set
        {
            runtimeScore = value;
            scoreOrLife_changedEvent.Raise();
        }
    }

    [SerializeField]
    private int initialLife;
    private int runtimeLife;
    public int Life
    {
        get => runtimeLife;
        private set
        {
            runtimeLife = value;
            scoreOrLife_changedEvent.Raise();
        }
    }

    public void OnAfterDeserialize()
    {
        GameState = initialGameState;
        Score = initialScore;
        Life = initialLife;
    }

    public void OnBeforeSerialize() { }

    // Fruit cut
    public void IncreaseScore()
    {
        Score += 10;
        Debug.Log("Score = " + Score);
    }

    // Wrong fruit cut
    public void DecreaseScore()
    {
        if (Score - 3 <= 0)
        {
            Score = 0;
            DecreaseLife();
        }
        else
        {
            Score -= 3;
        }
        Debug.Log("Score = " + Score);
    }

    // Fruit miss
    public void DecreaseLife()
    {
        if (Life - 1 <= 0)
        {
            Life = 0;
            GameState = GameState.End;
        }
        else
        {
            Life -= 1;
        }
        Debug.Log("Life = " + Life);
    }

    public void ResetScore()
    {
        Score = initialScore;
        Life = initialLife;
    }
}
