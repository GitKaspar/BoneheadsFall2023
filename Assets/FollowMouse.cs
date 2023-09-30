using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private bool isFollowing = false;
    private float lastRotationTime;
    public float rotationCooldown = 0.2f; // Adjust this cooldown time as needed

    void Update()
    {
        if (isFollowing)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the GameObject's position to the mouse position
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            // Check for scroll wheel input and cooldown time
            if (scrollDelta != 0f && Time.time - lastRotationTime >= rotationCooldown)
            {
                // Rotate the GameObject based on scroll direction
                float rotationAmount = scrollDelta > 0f ? 90f : -90f;
                transform.Rotate(Vector3.forward * rotationAmount);
                lastRotationTime = Time.time; // Update the last rotation time
            }
        }
    }

    void OnMouseDown()
    {
        // Toggle following when the GameObject is clicked
        isFollowing = !isFollowing;
    }
}