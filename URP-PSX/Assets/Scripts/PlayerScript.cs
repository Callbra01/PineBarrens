using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Movement variables
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float strafeSpeed = 7f;
    public bool canMove = true;
    //Vector3 to store calculations
    private Vector3 movement;

    //Rigidbody
    private Rigidbody rb;

    //Jump stuff
    [Header("Jump")]
    public float jumpHeight = 7f;
    public bool isGrounded;
    public LayerMask groundLayer;

    public AudioSource[] audioSources;

    int shoesCollected = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = CheckGround();

        if (isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3.Normalize(movement);

        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);

        if (canMove)
            rb.velocity += movement * Time.deltaTime;
        //rb.MovePosition(transform.position + movement * Time.deltaTime);
        */

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on input and camera/player orientation
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Apply velocity, maintaining current y-velocity for jumping/falling
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    //Function is of type bool
    bool CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.25f, groundLayer))
        {
            return true;
        }

        return false;
    }

    void PlaySource(int sourceIndex)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Stop();
        }

        audioSources[sourceIndex].Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelExit")
        {
            SceneManager.LoadScene("TestExit");
        }

        if (other.gameObject.tag == "WhiteLightTrigger")
        {
            PlaySource(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                Switch switchComp = other.GetComponent<Switch>();
                switchComp.isEnabled = !switchComp.isEnabled;
            }
        }

        if (other.gameObject.tag == "Shoe")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                if (other.GetComponent<Coffin>().isSolved)
                {
                    Debug.Log("COFFIN");
                    shoesCollected++;
                    other.gameObject.SetActive(false);
                }
            }
        }
    }
}
