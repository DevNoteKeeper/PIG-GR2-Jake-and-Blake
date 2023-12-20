using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    public GameObject jackPlayer;
    public GameObject blakePlayer;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (jackPlayer == null || blakePlayer == null)
        {
            Debug.LogError("Jack or Blake GameObject is not assigned!");
            return;
        }
    }

    void Update()
    {
        // Check if Jack and Blake exist
        if (jackPlayer != null && blakePlayer != null)
        {
            // Calculate distance between Jack and Blake in x and y axes
            float distanceX = Mathf.Abs(jackPlayer.transform.position.x - blakePlayer.transform.position.x);
            float distanceY = Mathf.Abs(jackPlayer.transform.position.y - blakePlayer.transform.position.y);

            // Set the LineRenderer positions based on the character positions
            lineRenderer.SetPosition(0, jackPlayer.transform.position);
            lineRenderer.SetPosition(1, blakePlayer.transform.position);

            // Output the distances to the console
            Debug.Log("DistanceX: " + distanceX + ", DistanceY: " + distanceY);
        }
    }
}
