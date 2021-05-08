using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitTypeGameEvent", menuName = "CustomSO/Events/FruitType_gameEvent")]
public class FruitType_gameEvent : ScriptableObject
{
    private List<FruitType_gameEventListener> listeners = new List<FruitType_gameEventListener>();

    public void RegisterListener(FruitType_gameEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void UnregisterListener(FruitType_gameEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }

    public void Raise(FruitType fruitTyoe)
    {
        for (int i = listeners.Count - 1; i >= 0; --i)
        {
            listeners[i].RaiseEvent(fruitTyoe);
        }
    }
}
