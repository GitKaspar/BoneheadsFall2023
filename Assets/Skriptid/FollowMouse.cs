using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject spawnerObj;
     Spawner spawner;

    private bool isFollowing = false;
    private float lastRotationTime;
    public float rotationCooldown = 0.1f; // Adjust this cooldown time as needed
    private Grupp _grupp;

    public bool canMove = true;

    void Start()
    {
        spawner = spawnerObj.GetComponent<Spawner>();
        _grupp = GetComponent<Grupp>();
    }

    void Update()
    {


        if (isFollowing)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the GameObject's position to the mouse position
            // Snap siin: hiire positsioon ümardatakse
            if (_grupp.KasOnSobivAsendRuudustikus())
            {
                Vector2 ümardatud = Mänguväli.ÜmardaVector2(new Vector2(mousePosition.x, mousePosition.y));
                transform.position = new Vector3(ümardatud.x, ümardatud.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            }

            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            // Check for scroll wheel input and cooldown time
            if (scrollDelta != 0f && Time.time - lastRotationTime >= rotationCooldown)
            {
                // Rotate the GameObject based on scroll direction
                float rotationAmount = scrollDelta > 0f ? 90f : -90f;
                transform.Rotate(Vector3.forward * rotationAmount);
                lastRotationTime = Time.time; // Update the last rotation time
            }
            /*
             * Probleem: 
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<Spawner>().SpawnNext();
            }
            */
        }
    }

    void OnMouseDown()
    {
        if (_grupp.KasOnSobivAsendRuudustikus())
        {
            if (canMove)
            {
                if (isFollowing)
                {
                    canMove = false;
                    spawner.SpawnNext();
                }
             }
            // Toggle following when the GameObject is clicked
            isFollowing = !isFollowing;
        }
    }
}
