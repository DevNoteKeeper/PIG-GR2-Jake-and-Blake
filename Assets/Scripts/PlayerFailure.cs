using UnityEngine;

public class PlayerFailure : MonoBehaviour
{
    public Player movement;

    void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "" ){
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
