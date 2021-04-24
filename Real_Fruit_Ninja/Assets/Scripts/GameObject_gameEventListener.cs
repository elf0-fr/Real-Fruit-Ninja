using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObject_unityEvent: UnityEvent<GameObject> { }

public class GameObject_gameEventListener : MonoBehaviour
{
    public GameObject_gameEvent GameEvent;

    public GameObject_unityEvent Response;

    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    public void RaiseEvent(GameObject gameObject)
    {
        Response.Invoke(gameObject);
    }
}
