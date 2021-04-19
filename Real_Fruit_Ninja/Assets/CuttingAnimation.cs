using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 7.0f;
    private Vector3 tagetPos;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private TrailRenderer childTrail;

    public void Start() 
    {   
        trail.enabled = false;
        childTrail.enabled = false;
        Random.InitState(System.DateTime.Now.Millisecond);
        Vector3 randomDirection = new Vector3(0.0f, 0.0f, Random.value);
        transform.Rotate(randomDirection);
        transform.Translate(transform.right * 2);

        tagetPos = transform.position + transform.right * 4;
        trail.enabled = true;
        childTrail.enabled = true;
    }


    public void Update()
    {
        float step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,tagetPos , step);

    }
}
