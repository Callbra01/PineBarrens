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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3.Normalize(movement);

        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);

        if (canMove)
            rb.MovePosition(transform.position + movement * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelExit")
        {
            SceneManager.LoadScene("TestExit");
        }
    }
}
