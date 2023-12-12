using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 12f;

    [SerializeField] float moveAmount;
    [SerializeField] float jumpAmount;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.Translate(5,5,0);
        moveAmount = Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
        jumpAmount = Input.GetAxis("Vertical")*jumpSpeed*Time.deltaTime;
        transform.Translate(moveAmount,jumpAmount,0);
    }
}
