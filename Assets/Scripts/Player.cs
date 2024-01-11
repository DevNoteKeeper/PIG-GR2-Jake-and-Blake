using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float walk = 0;
    [SerializeField]
    float toward = 1f;
    [SerializeField]
    public float fixedJumpHeight = 5f;
    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;
    private bool isGround = true;
    private bool isOnSnowLand = false;
    Rigidbody2D rigid;
    Animator anim;


    public GameObject jackPlayer;  // Assign the GameObject of Jack in the Unity Editor
    public GameObject blakePlayer; // Assign the GameObject of Blake in the Unity Editor

    [SerializeField]
    private float maxDistance = 5f;

    

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

        AdjustDistance();

    }

    void FixedUpdate() {
        // bool isOnSnowLand = false;
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,0.2f);
        // foreach(Collider2D collider in colliders){
        //     if(collider.CompareTag("snowLand")){
        //         isOnSnowLand=true;
        //         break;
        //     }
        // }
        float speedMultiplier = isOnSnowLand ? 5.0f : 0.5f;
        rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        transform.position += new Vector3(walk*speedMultiplier, 0, 0);

        if(rigid.position.y<-5f){
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Walk(){

        walk = 0f;
        anim.SetBool("isWalk", false);

        if(tag == "Jake"){
            if(Input.GetKey(KeyCode.A)) { 
            walk = -0.05f;
            toward = -1f;
            anim.SetBool("isWalk", true);
            
        } else if(Input.GetKey(KeyCode.D)){
            walk = 0.05f;
            toward = 1f;
            anim.SetBool("isWalk", true);
        }
        } else if(tag == "Blake"){
            if(Input.GetKey(KeyCode.LeftArrow)) { 
            walk = -0.05f;
            toward = -1f;
            anim.SetBool("isWalk", true);
            
        } else if(Input.GetKey(KeyCode.RightArrow)){
            walk = 0.05f;
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
}

        void AdjustDistance()
    {
        if (blakePlayer != null)
        {
            float distance = Mathf.Abs(transform.position.x - blakePlayer.transform.position.x);

            if (distance > maxDistance)
            {
                float adjustment = distance - maxDistance;
                transform.position = new Vector3(transform.position.x - adjustment * Mathf.Sign(transform.position.x - blakePlayer.transform.position.x), transform.position.y, transform.position.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            anim.SetBool("isSnowLand", false);
            isOnSnowLand=false;
           
        }else if(other.gameObject.CompareTag("snowLand")){
            isOnSnowLand=true;
            anim.SetBool("isSnowLand", true);
            
        }
    }


}
