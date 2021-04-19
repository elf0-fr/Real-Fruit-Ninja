using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField]
    private float defaultTime;

    private float time;

    [SerializeField]
    private Vector2 fallForce;

    [SerializeField]
    private bool isCut = false;

    private bool isFruitVisible = false;


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

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //TODO choisir un fruit de façon random et l'assigner aux sprit renderer
        //TODO choisir un angle de tir correcte de façon random ?

        time = defaultTime;
    }

    void FixedUpdate()
    {

        if (time > 0f)
        {
            rb2D_fruit.AddForce(new Vector2(5f, 10f));
            rb2D_fruitA.AddForce(new Vector2(5f, 10f));
            rb2D_fruitB.AddForce(new Vector2(5f, 10f));
            time -= Time.deltaTime;
        }
        else
        {
            Cut();

            rb2D_fruit.AddForce(fallForce);
            rb2D_fruitA.AddForce(fallForce);
            rb2D_fruitB.AddForce(fallForce);
        }

        if (isCut)
        {
            rb2D_fruitA.AddForce(new Vector2(1f, 0f));
            rb2D_fruitB.AddForce(new Vector2(-1f, 0f));
        }

        IsFruitVisible();
    }

    public void Cut()
    {
        sR_fruit.enabled = false;

        //TODO rajouter un point
    }

    private void IsFruitVisible()
    {
        if (!sR_fruit.isVisible && !sR_fruitA.isVisible && !sR_fruitB.isVisible && isFruitVisible)
        {
            if (!isCut)
            {
                //TODO rajouter une pénalité
            }

            fruit.transform.position = gameObject.transform.position;
            fruitA.transform.position = gameObject.transform.position;
            fruitB.transform.position = gameObject.transform.position;

            sR_fruit.enabled = true;
            isFruitVisible = false;

            gameObject.SetActive(false);
        }
        else if (sR_fruit.isVisible)
        {
            isFruitVisible = true;
        }
    }
}
