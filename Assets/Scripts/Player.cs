using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed = 0.05f;
    private float walk = 0;
    private float toward = 1f;
    [SerializeField]
    public float fixedJumpHeight = 5f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    private bool isGround = true;
    private bool isOnSnowLand = false;
    private Rigidbody2D rigid;
    private Animator anim;

    public GameObject jackPlayer;  // Assign the GameObject of Jack in the Unity Editor
    public GameObject blakePlayer; // Assign the GameObject of Blake in the Unity Editor


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }

    void FixedUpdate()
    {
        // bool isOnSnowLand = false;
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,0.2f);
        // foreach(Collider2D collider in colliders){
        //     if (collider.CompareTag("snowLand")){
        //         isOnSnowLand=true;
        //         break;
        //     }
        // }
        float speedMultiplier = isOnSnowLand ? 5.0f : 1.0f;
        rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        transform.position += new Vector3(walk * speedMultiplier, 0, 0);
    }

    void Walk()
    {
        walk = 0f;
        anim.SetBool("isWalk", false);

        if (tag == "Jake")
        {
            if (Input.GetKey(KeyCode.A))
            {
                walk = -walkSpeed;
                toward = -1f;
                anim.SetBool("isWalk", true);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                walk = walkSpeed;
                toward = 1f;
                anim.SetBool("isWalk", true);
            }
        }
        else if (tag == "Blake")
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                walk = -walkSpeed;
                toward = -1f;
                anim.SetBool("isWalk", true);

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                walk = walkSpeed;
                toward = 1f;
                anim.SetBool("isWalk", true);
            }
        }

        transform.localScale = new Vector3(toward, 1, 1);
    }

    void Jump()
    {
        if ((tag == "Jake" && Input.GetKeyDown(KeyCode.Space) && isGround) || (tag == "Blake" && Input.GetKeyDown(KeyCode.UpArrow) && isGround))
        {
            anim.SetBool("isWalk", true);
            rigid.velocity = new Vector2(rigid.velocity.x, fixedJumpHeight);
            isGround = false;
        }

        if (rigid.velocity.y < 0)
        {
            anim.SetBool("isWalk", true);
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow))
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (isOnGround())
        {
            isGround = true;
        }
    }

    bool isOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        return hit.collider != null && hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Box");
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box"))
        {
            isGround = true;
            anim.SetBool("isSnowLand", false);
            isOnSnowLand = false;
        }
        else if (other.gameObject.CompareTag("snowLand"))
        {
            isOnSnowLand = true;
            anim.SetBool("isSnowLand", true);
        }
    }
}
