using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] int movementSpeed;
    [SerializeField] GameObject border;

    private Vector2 movementInput;
    private BoxCollider2D myBoxCollider2D;

    void Start()
    {
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement();

        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Shaks")))
        {
            PLayerHit();
            Debug.Log("hit");

        }
    }
    public void PLayerHit()
    {
        FindObjectOfType<GameSession>().TakeLive();
    }
    private void movement()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();

        playerRB.velocity = movementInput * movementSpeed;
    }
}
