using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    //Mouse movement variables
    public float mouseSensitivity = 5f;

    //For smooth movement
    public float smoothing = 1.5f;

    //Two vectors to store calculations
    private Vector2 mouseLook;
    private Vector2 smoothMovement;

    //Reference to the player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the player
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //This will make the cursor invisible. Hit the ESC key to get it back
        Cursor.lockState = CursorLockMode.Locked;

        //Local variable for mouse movement
        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"),
                                Input.GetAxis("Mouse Y"));

        //Multiply whatever the Mouse input is by sensitivity and smooooooooth factor.
        mouseDirection.x *= mouseSensitivity * smoothing;
        mouseDirection.y *= mouseSensitivity * smoothing;

        //Lerp (linear interpolation) between two positions. Here we are moving between where the
        //mouse currently is, and the above calculated position.
        //Move between current x, calculated x at a speed of 1 / smoothing 
        //(that is a cheap way of normalizing)
        smoothMovement.x = Mathf.Lerp(smoothMovement.x, mouseDirection.x, 1f / smoothing);
        smoothMovement.y = Mathf.Lerp(smoothMovement.y, mouseDirection.y, 1f / smoothing);

        //Now we can add those two calculations together!
        mouseLook += smoothMovement;

        //Clamp the mouse position so the player can't rotate infinitely on the x-axis
        mouseLook.y = Mathf.Clamp(mouseLook.y, -80f, 90f);

        //Rotate the camera to the newly calculated position.
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        //Move the player object on the x-axis only
        player.transform.rotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}
