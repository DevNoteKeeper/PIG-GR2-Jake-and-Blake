using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slipper : MonoBehaviour
{
    public float slipFactor = 0.5f; 
    public string[] validTags = { "Blake", "Jake" }; 

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsTagValid(collision.collider.tag))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                Vector2 slipForce = new Vector2(playerRb.velocity.x * slipFactor, 0f);
                playerRb.AddForce(slipForce, ForceMode2D.Force);
            }
        }
    }

    private bool IsTagValid(string tag)
    {
        foreach (string validTag in validTags)
        {
            if (tag == validTag)
            {
                return true;
            }
        }
        return false;
    }
}
