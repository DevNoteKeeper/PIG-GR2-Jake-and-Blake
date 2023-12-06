using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;

    [SerializeField] private float m_MoveSpeed = 5.0f;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_RigidBody.velocity = new Vector2(0, m_MoveSpeed);
        }
    }
}
