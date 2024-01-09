using UnityEngine;

public class Slippery : MonoBehaviour
{
    public Player player;
    private Animator animator;
    public Rigidbody2D rigid;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.collider.tag=="snowLand"){
            animator.SetFloat("Speed",5f);
        }else{
            animator.SetFloat("Speed",1f);
        }

    }
}
