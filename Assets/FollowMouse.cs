using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the GameObject's position to the mouse position
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    void OnMouseDown()
    {
        // Toggle following when the GameObject is clicked
        isFollowing = !isFollowing;
    }
}