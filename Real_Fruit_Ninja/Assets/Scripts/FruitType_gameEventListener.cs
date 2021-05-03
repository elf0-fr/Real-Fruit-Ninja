using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FruitType_unityEvent : UnityEvent<FruitType> { }

public class FruitType_gameEventListener : MonoBehaviour
{
    public FruitType_gameEvent GameEvent;

    public FruitType_unityEvent Response;

    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    public void RaiseEvent(FruitType fruitType)
    {
        Response.Invoke(fruitType);
    }
}
