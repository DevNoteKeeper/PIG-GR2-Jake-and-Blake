using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            DestroyBox(collision.gameObject);

            Debug.Log("Destroyed a box after collision: " + collision.gameObject.name);
        }
    }

    private void DestroyBox(GameObject box)
    {
        // Immediately destroy the box object.
        Destroy(box);
    }
}
