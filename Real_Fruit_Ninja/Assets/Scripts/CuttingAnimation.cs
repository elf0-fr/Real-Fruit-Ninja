using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    private Vector3 tagetPos;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private TrailRenderer childTrail;
    private float timer = 0.0f;

    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject fruitcut1;
    [SerializeField] private GameObject fruitcut2;
    public void Start() 
    {   
        timer=0.0f;
        trail.enabled = false;
        childTrail.enabled = false;
        transform.localPosition  = new Vector3(-0.25f,-0.25f,0.0f);
        tagetPos = transform.localPosition + new Vector3(+0.5f,+0.5f,0.0f);
        trail.enabled = true;
        childTrail.enabled = true;
    }


    public void Update()
    {
        timer+=Time.deltaTime;
        if(timer>0.5f){
            float step =  speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,tagetPos , step);
        }
        if(transform.localPosition == tagetPos)
        {
            fruit.SetActive(false);
            fruitcut1.SetActive(true);
            fruitcut2.SetActive(true);
        }
    }
}
