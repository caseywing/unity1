using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaks : MonoBehaviour
{

    [SerializeField] float SharksSpeed;
    [SerializeField] float playerChaseRange;

    private Rigidbody2D SharksRB;
    private Vector3 directionToMoveIn;
    private Transform playerToChase;

    void Start()
    {
        SharksRB = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<twoDMovement>().transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else
        {
            directionToMoveIn = Vector3.zero;
        }

        directionToMoveIn.Normalize();
        SharksRB.velocity = directionToMoveIn * SharksSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
    }
}
