using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "CustomSO/Events/GameObject_gameEvent")]
public class GameObject_gameEvent : ScriptableObject
{
    private List<GameObject_gameEventListener> listeners = new List<GameObject_gameEventListener>();

    public void RegisterListener(GameObject_gameEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void UnregisterListener(GameObject_gameEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }

    public void Raise(GameObject gameObject)
    {
        for (int i = listeners.Count - 1; i >= 0; --i)
        {
            listeners[i].RaiseEvent(gameObject);
        }
    }
}
