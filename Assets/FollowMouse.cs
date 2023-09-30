using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private bool isFollowing = false;

    public GameObject klots;

    void Update()
    {
        if (isFollowing)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the GameObject's position to the mouse position
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            if (Input.GetMouseButtonDown(1)) // Check for right-click
            {
                // Rotate the GameObject 90 degrees clockwise
                transform.Rotate(Vector3.forward * 90f);
            }
        }


    }

    void OnMouseDown()
    {
        // Toggle following when the GameObject is clicked
        isFollowing = !isFollowing;
    }
}