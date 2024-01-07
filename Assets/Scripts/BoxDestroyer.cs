using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            DestroyBox(collision.gameObject);
        }
    }

    private void DestroyBox(GameObject box)
    {
        // Immediately destroy the box object.
        Destroy(box);

        Debug.Log("Destroyed box after a collision: " + box.name);
    }
}
