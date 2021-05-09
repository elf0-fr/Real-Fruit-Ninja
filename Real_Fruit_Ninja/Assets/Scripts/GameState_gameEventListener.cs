using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameState_unityEvent : UnityEvent<GameState> { }

public class GameState_gameEventListener : MonoBehaviour
{
    public GameState_gameEvent GameEvent;

    public GameState_unityEvent Response;

    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    public void RaiseEvent(GameState state)
    {
        Response.Invoke(state);
    }
}
