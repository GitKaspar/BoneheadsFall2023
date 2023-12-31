using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public static FollowMouse instance;

    public GameObject spawnerObj;
     Spawner spawner;

    private bool isFollowing = false;
    private float lastRotationTime;
    public float rotationCooldown = 0.1f; // Adjust this cooldown time as needed
    private Grupp _grupp;

    private Vector2 catVec;
    public bool canMove = true;

    void Start()
    {

        catVec = new Vector2(this.transform.position.x, this.transform.position.y);

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
            if (_grupp.IsValidPosition())
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
       
        }

        // See osa peaks liitma mahapandud klotsi keelatud tsooniga ja lülitama klotsi välja
        if (!canMove) 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform squareTransform = transform.GetChild(i);
                Debug.Log(new Vector2(squareTransform.position.x, squareTransform.position.y), this);
                Mänguväli._keelatudRuudud.Add(new Vector2(squareTransform.position.x, squareTransform.position.y));
            }
            this.enabled = false;
        }
    }


    void OnMouseDown()
    {

        if (_grupp.IsValidPosition(catVec))
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
