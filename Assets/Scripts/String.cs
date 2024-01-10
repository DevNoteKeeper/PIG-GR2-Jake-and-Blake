using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondController : MonoBehaviour
{
    public GameObject jakePlayer;
    public GameObject blakePlayer;
    private Transform stringTransform;

    void Start()
    {
        stringTransform = this.transform;
    }

    void Update()
    {
        UpdateStringBond();
    }

    void UpdateStringBond()
    {
        Vector3 pos1 = jakePlayer.transform.position;
        Vector3 pos2 = blakePlayer.transform.position;

        // Position the string at the midpoint between the two characters
        stringTransform.position = (pos1 + pos2) / 2;

        // Scale the string based on the distance between the two characters
        float distance = Vector3.Distance(pos1, pos2);
        stringTransform.localScale = new Vector3(distance, 1, 1);

        // Rotate the string to face from character1 to character2
        Vector3 relativePos = pos2 - pos1;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        stringTransform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
    }

}