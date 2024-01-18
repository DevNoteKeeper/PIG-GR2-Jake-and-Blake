using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    // Reference to the Jack and Blake transforms
    public Transform Jack;
    public Transform Blake;

    // Reference to the LineRenderer component
    private LineRenderer lineRenderer;

    // Color and maximum length settings for the line
    public Color lineColor = Color.green;
    public float maxLineLength = 15f;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configure LineRenderer properties
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = lineColor;
    }

    void Update()
    {
        // If Jack or Blake is null, abort the method
        if (Jack == null || Blake == null)
        {
            return;
        }

        // Set the starting point of the line to Jack's position
        lineRenderer.SetPosition(0, Jack.position);

        // Calculate the distance between the characters
        float distance = Vector3.Distance(Jack.position, Blake.position);

        // If the distance between characters is greater than the maximum length, set the line to the maximum length
        if (distance > maxLineLength)
        {
            // Keep the line at the maximum length while moving Jack and Blake
            Vector3 direction = (Blake.position - Jack.position).normalized;
            Vector3 newBlakePosition = Jack.position + direction * maxLineLength;
            Blake.position = newBlakePosition;

            // Update the line
            lineRenderer.SetPosition(1, newBlakePosition);
        }
        else
        {
            // Update the line based on the positions of the characters
            lineRenderer.SetPosition(1, Blake.position);
        }
    }
}
