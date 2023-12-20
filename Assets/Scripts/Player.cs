using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    float walk = 0;
    [SerializeField]
    float toward = 1f;
    [SerializeField]
    public float fixedJumpHeight = 5f;
    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;
    private bool isGround = true;

    Rigidbody2D rigid;
    Animator anim;

    public Transform otherPlayer;
    private DistanceJoint2D distanceJoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint.connectedBody = otherPlayer.GetComponent<Rigidbody2D>();
        distanceJoint.autoConfigureDistance = false; // Use manual distance configuration

        // Set both x and y axis distance limit
        distanceJoint.distance = Vector2.Distance(transform.position, otherPlayer.position);
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }

    void FixedUpdate() {
        rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        transform.position += new Vector3(walk, 0, 0);
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

    void Jump() {
        
            if(tag == "Jake"){
                if (Input.GetKey(KeyCode.Space) && isGround){
                anim.SetBool("isWalk", true);
                rigid.velocity = new Vector2(rigid.velocity.x, fixedJumpHeight);

                // If characters take chracter's foot off, "isGround" changed false
                isGround = false;
            }
            } else if(tag == "Blake"){
                if (Input.GetKey(KeyCode.UpArrow) && isGround){
                anim.SetBool("isWalk", true);
                rigid.velocity = new Vector2(rigid.velocity.x, fixedJumpHeight);

                // If characters take chracter's foot off, "isGround" changed false
                isGround = false;
            }
            }
            

            if (rigid.velocity.y < 0){
                anim.SetBool("isWalk", true);
                rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space)){
                rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {

            // A "Ground" tag is required to be given to the ground and box where the character is standing.
            if (other.gameObject.CompareTag("Ground")) {
                isGround = true;
            }
        }

}
