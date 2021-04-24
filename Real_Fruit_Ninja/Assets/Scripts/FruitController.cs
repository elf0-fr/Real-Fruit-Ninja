using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    //TODO ajout du type de fuit.

    [SerializeField]
    private float defaultTime;
    private float time;
    public Vector2 JumpForce;
    [SerializeField]
    private Vector2 fallForce;

    [SerializeField]
    private bool isCut = false;
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
    private SpriteRenderer sR_fruit;
    [SerializeField]
    private SpriteRenderer sR_fruitA;
    [SerializeField]
    private SpriteRenderer sR_fruitB;

    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody2D rb2D_fruit;
    [SerializeField]
    private Rigidbody2D rb2D_fruitA;
    [SerializeField]
    private Rigidbody2D rb2D_fruitB;

    private void OnEnable()
    {
        time = defaultTime;
        IsWaiting = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isFruitVisible)
        {
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

        CheckFruitVisible();
    }

    public void Cut()
    {
        if (IsWaiting || isCut)
            return;

        sR_fruit.enabled = false;
        isCut = true;

        fruitCut_event.Raise();
    }

    private void CheckFruitVisible()
    {
        if (!sR_fruit.isVisible && !sR_fruitA.isVisible && !sR_fruitB.isVisible && isFruitVisible)
        {
            if (!isCut)
                fruitMissed_event.Raise();

            fruit.transform.position = gameObject.transform.position;
            fruitA.transform.position = gameObject.transform.position;
            fruitB.transform.position = gameObject.transform.position;

            sR_fruit.enabled = true;
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
