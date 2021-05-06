using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    BANANA,
    APPLE,
    CARROT,
    PEAR,
    TOMATO,

    FRUITS = 5
}

public class FruitController : MonoBehaviour
{
    [SerializeField]
    private float defaultTime;
    private float time;
    public Vector2 JumpForce;
    [SerializeField]
    private Vector2 fallForce;

    [SerializeField]
    private bool isCut = false;
    public bool IsCut => isCut;
    [SerializeField]
    private bool isFruitVisible = false;
    public bool IsWaiting = true;


    [Header("GameEvent")]
    [SerializeField]
    private GameEvent fruitCut_event;
    [SerializeField]
    private GameEvent fruitMissed_event;
    [SerializeField]
    private GameObject_gameEvent fruitDespawn_event;


    [Header("GameObject")]
    [SerializeField]
    private GameObject fruit;
    [SerializeField]
    private GameObject fruitA;
    [SerializeField]
    private GameObject fruitB;

    [Header("Sprite Renderer")]
    [SerializeField]
    public SpriteRenderer sR_fruit;
    [SerializeField]
    public SpriteRenderer sR_fruitA;
    [SerializeField]
    public SpriteRenderer sR_fruitB;

    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody2D rb2D_fruit;
    [SerializeField]
    private Rigidbody2D rb2D_fruitA;
    [SerializeField]
    private Rigidbody2D rb2D_fruitB;

    public FruitType fruitType;

    private float rotation_ratio;

    private void OnEnable()
    {
        time = defaultTime;
        IsWaiting = true;
        rotation_ratio = Random.Range(-1.0f, 1.0f);
    }

    void Update()
    {
        Debug.Log(isFruitVisible);
        if (Input.GetKeyDown(KeyCode.C) && isFruitVisible)
        {
            IsWaiting = false;
            Cut();
        }
    }

    void FixedUpdate()
    {
        if (IsWaiting)
        {
            return;
        }

        if (time > 0f)
        {
            rb2D_fruit.AddForce(JumpForce);
            rb2D_fruitA.AddForce(JumpForce);
            rb2D_fruitB.AddForce(JumpForce);
            time -= Time.deltaTime;
        }
        else
        {
            rb2D_fruit.AddForce(fallForce);
            rb2D_fruitA.AddForce(fallForce);
            rb2D_fruitB.AddForce(fallForce);
        }

        if (isCut)
        {
            rb2D_fruitA.AddForce(new Vector2(1f, 0f));
            rb2D_fruitB.AddForce(new Vector2(-1f, 0f));
        }

        else
        {
            fruit.transform.Rotate(0.0f, 0.0f, fruit.transform.rotation.z - rotation_ratio * 90.0f * Time.deltaTime, Space.World);
            fruitA.transform.Rotate(0.0f, 0.0f, fruit.transform.rotation.z - rotation_ratio * 90.0f * Time.deltaTime, Space.World);
            fruitB.transform.Rotate(0.0f, 0.0f, fruit.transform.rotation.z - rotation_ratio * 90.0f * Time.deltaTime, Space.World);
        }

        CheckFruitVisible();
    }

    public void Cut()
    {
        if (IsWaiting || isCut)
            return;

        sR_fruit.enabled = false;
        sR_fruitA.enabled = true;
        sR_fruitB.enabled = true;
        rb2D_fruit.angularVelocity = 0.0f;
        float r = Random.Range(-1.0f, 1.0f);
        rb2D_fruitB.AddTorque(r * 5.0f, ForceMode2D.Impulse);
        rb2D_fruitA.AddTorque(-5.0f * r , ForceMode2D.Impulse);
        isCut = true;
        IsWaiting = true;

        fruitCut_event.Raise();
    }

    private void CheckFruitVisible()
    {
        if (!sR_fruit.isVisible && !sR_fruitA.isVisible && !sR_fruitB.isVisible && isFruitVisible)
        {
            if (!isCut)
                fruitMissed_event.Raise();

            fruit.transform.position = gameObject.transform.position;

            //Portion modifiée
            fruitA.transform.localPosition = new Vector3(0.61f, 0.0f, 0.0f);
            fruitB.transform.localPosition = new Vector3(-0.61f, 0.0f, 0.0f);


            sR_fruit.enabled = true;

            //Portion rajoutée
            fruitA.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 7.16f));
            fruitB.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -7.16f));
            sR_fruitA.enabled = false;
            sR_fruitB.enabled = false;

            isFruitVisible = false;
            isCut = false;

            fruitDespawn_event.Raise(this.gameObject);

            gameObject.SetActive(false);
        }
        else if (sR_fruit.isVisible)
        {
            isFruitVisible = true;
        }
    }
}
