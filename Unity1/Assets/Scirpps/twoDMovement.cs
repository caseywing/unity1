using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDMovement : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] GameObject border;
    [SerializeField] float timeOnOff = 0.14f;
    private bool OnOff = false;
    private bool TurnOn;
    private bool imortal = false;
    private bool keepTouching = false;

    Rigidbody2D playerRB;
    private BoxCollider2D myBoxCollider2D;
    private float shotCounter;
    float horizontalInput;
    float verticalInput;
    
    Animator animator;

    string currentState;
    const string player_IDLE = "idling1";
    const string player_up = "swimmingup";
    const string player_down = "swimmingdown";
    const string player_right = "swimmingright";
    const string player_left = "swimmingleft";
    const string player_upperLeft = "swimmingupleft";
    const string player_upperRight = "swimmingupright";
    const string player_downLeft = "swimmingdownleft";
    const string player_downRight = "swimmingdownright";



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
        OnOff = imortal;
        
        gameObject.GetComponent<SpriteRenderer>().enabled = TurnOn;
        border.transform.position = border.transform.position = new Vector3(border.transform.position.x, gameObject.transform.position.y);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Shaks")))
        {
            PLayerHit();
            keepTouching = true;
        }
        else
        {
            keepTouching = false;
        }

        if (keepTouching)
        {
            StartCoroutine(BeingImortal());
        }

    }
    void FixedUpdate()
    {
        GhatGPTMovementScript();
    }
    private void GhatGPTMovementScript()
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to ensure consistent speed in all directions
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        playerRB.velocity = movement * moveSpeed;

        if (movement.x > 0 && movement.y == 0)
        {
            ChangeAnimationState(player_right);
        }
        else if (movement.x < 0 && movement.y == 0)
        {
            ChangeAnimationState(player_left);
        }
        else if (movement.x == 0 && movement.y > 0)
        {
            ChangeAnimationState(player_up);
        }
        else if (movement.x == 0 && movement.y < 0)
        {
            ChangeAnimationState(player_down);
        }
        else if (movement.x != 0 && movement.y != 0)
        {
            if (movement.x > 0 && movement.y > 0)
            {
                ChangeAnimationState(player_upperRight);
            }
            else if (movement.x < 0 && movement.y > 0)
            {
                ChangeAnimationState(player_upperLeft);
            }
            else if (movement.x > 0 && movement.y < 0)
            {
                ChangeAnimationState(player_downRight);
            }
            else if (movement.x < 0 && movement.y < 0)
            {
                ChangeAnimationState(player_downLeft);
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

    IEnumerator BeingImortal()
    {

        imortal = true;

        if (keepTouching)
        {
            yield return new WaitForSeconds(GameObject.Find("Game Session").GetComponent<GameSession>().TouchCounter);

            imortal = false;
        }
    }

}
