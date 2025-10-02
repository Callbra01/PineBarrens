using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement variables
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float strafeSpeed = 7f;

    //Vector3 to store calculations
    private Vector3 movement;

    //Rigidbody
    private Rigidbody rb;

    //Jump stuff
    [Header("Jump")]
    public float jumpHeight = 7f;
    public bool isGrounded;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the RB
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Use a separate function for the isGrounded check
        isGrounded = CheckGround();

        //If the player is on the ground, jumping is allowed.
        if (isGrounded)
        {
            Jump();
        }
    }

    //FixedUpdate() gets called once per frame at a fixed interval
    private void FixedUpdate()
    {
        //Create two temporary float variables to hold the Inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        /*Now we can use our calculator variable!
         * Set the Vector3 called movement to whatever transform.forward is (z-axis) then
         * multiply it by v. GetAxis can be -1, 0 or 1 then multiply THAT by moveSpeed.
         * Same thing for horizontal, but we are multiplying by transform.right (x-axis) and strafeSpeed.
         * 
         * BEFORE WE DO ANYTHING. We need to Normalize it (scale it to a number between 0 and 1)
         */
        Vector3.Normalize(movement);

        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);

        //Use rb.MovePosition. It is taking current position (transform.position) and adding the
        //Vector3 calculation above. Then scale by frameRate (Time.deltaTIme)
        rb.MovePosition(transform.position + movement * Time.deltaTime);
    }

    void Jump()
    {
        //If Jump() was called, then the player must be on the ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Add a force to the rigidbody
            //This is adding a force in the up direction (0, 1, 0) * jumpHeight.
            //ForceMode.Impulse means "the frame that it was called" AKA right away
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    //Function is of type bool
    bool CheckGround()
    {
        //RaycastHit is a special type of variable that stores the collision information of a Raycast
        RaycastHit hit;

        //Raycast is a line of collision.
        /*
         * Parameters are (starting point of the ray, direction of the ray, where to store
         * the results, length, layer to check)
         * 
         */
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.25f, groundLayer))
        {
            //If there is a collision, return true.
            return true;
        }

        //In all other scenarios, return false
        return false;
    }
}
