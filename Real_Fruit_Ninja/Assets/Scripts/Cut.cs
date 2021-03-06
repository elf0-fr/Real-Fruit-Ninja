using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    private Vector3 initialPos;
    private Vector3 tagetPos;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private TrailRenderer childTrail;
    [SerializeField] private FruitController fruit;
    private ParticleSystem part_sys;

    public void initialization()
    {
        trail.enabled = false;
        childTrail.enabled = false;
        transform.localPosition  = initialPos;
        part_sys.Stop();
    }

    void Awake()
    {
        part_sys = GetComponent<ParticleSystem>();
        initialPos = new Vector3(-0.3f,-0.3f,0.0f);
        tagetPos = initialPos + new Vector3(+2.7f,+2.7f,0.0f);
    }

    void Start() 
    {   
        initialization();
    }

    void Update()
    {
        if(fruit.IsCut && transform.localPosition == initialPos)
        {
            part_sys.Play();
        }
        if(fruit.IsCut && transform.localPosition != tagetPos){
            trail.enabled = true;
            childTrail.enabled = true;
            float step =  speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, tagetPos, step);
        }
        if(transform.localPosition == tagetPos){
            fruit.IsWaiting = false;
            trail.enabled = false;
            childTrail.enabled = false;
            part_sys.Stop();
        }
        if(transform.localPosition == tagetPos && !fruit.IsCut)
        {
            initialization();
        }
    }


}
