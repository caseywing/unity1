using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D playerRigidbody;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();

        playerRigidbody.velocity = movementInput * speed;
    }
}
