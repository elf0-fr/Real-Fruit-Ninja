using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameStateGameEvent", menuName = "CustomSO/Events/GameState_gameEvent")]
public class GameState_gameEvent : ScriptableObject
{
    private List<GameState_gameEventListener> listeners = new List<GameState_gameEventListener>();

    public void RegisterListener(GameState_gameEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void UnregisterListener(GameState_gameEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }

    public void Raise(GameState state)
    {
        for (int i = listeners.Count - 1; i >= 0; --i)
        {
            listeners[i].RaiseEvent(state);
        }
    }
}
