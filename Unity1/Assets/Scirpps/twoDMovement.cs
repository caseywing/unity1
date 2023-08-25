using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDMovement : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    [SerializeField] GameObject border;
    [SerializeField] float timeOnOff;
    [SerializeField] bool OnOff = true;
    [SerializeField] bool TurnOn;

    Rigidbody2D playerRB;
    private BoxCollider2D myBoxCollider2D;
    private float shotCounter;

    float walkSpeed = 10f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    Animator animator;

    string currentState;
    const string player_IDLE = "idling1";
    const string player_up = "swimmingup";
    const string player_down = "swimmingdown";
    const string player_right = "swimmingright";
    const string player_left = "swimmingleft";
    const string player_hit = "Immortal";


    void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        TurnOn = true;
        StartCoroutine(On());
    }
    void Update()
    {
        OnOff = GameObject.Find("Game Session").GetComponent<GameSession>().imortal;
        gameObject.GetComponent<SpriteRenderer>().enabled = TurnOn;
        border.transform.position = border.transform.position = new Vector3(border.transform.position.x, gameObject.transform.position.y);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Shaks")))
        {
            PLayerHit();
            GameObject.Find("Game Session").GetComponent<GameSession>().keepTouching = true;
        }
        else
        {
            GameObject.Find("Game Session").GetComponent<GameSession>().keepTouching = false;
        }

        
    }
    void FixedUpdate()
    {

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            playerRB.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);
            

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(player_right);
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(player_left);
            }
            else if (inputVertical > 0)
            {
                ChangeAnimationState(player_up);
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState(player_down);
            }


        }
        else
        {
            playerRB.velocity = new Vector2(0f, 0f);
            ChangeAnimationState(player_IDLE);
        }
    }
    public void PLayerHit()
    {
        FindObjectOfType<GameSession>().TakeLive();
        
    }
    
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }
    public IEnumerator On()
    {

        while (true)
        {
            if (OnOff)
            {
                TurnOn = !TurnOn;
            }
            else
            {
                TurnOn = true;
            }
            yield return new WaitForSeconds(timeOnOff);
        }
    }
}
