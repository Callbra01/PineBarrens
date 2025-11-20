using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance { get; private set; }

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

    public int shoesCollected = 0;

    bool _inSwitchTrigger = false;
    bool _inShoeTrigger = false;
    bool _inLegsTrigger = false;
    public bool _inDoveTrigger = false;

    public GameObject currentInteractable;

    public GameObject doveCollectable;
    public bool doveCollected = false;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

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

        Debug.Log(_inDoveTrigger);

        if (Input.GetKeyDown(KeyCode.F))
        {
            //GameManager.instance.isDoveIconActive = true;
        }

        HandleInteractable();
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

    private void HandleInteractable()
    {
        TriggerDoveIcon();

        if (currentInteractable == null)
        {
            return;
        }

        TriggerSwitch();
        TriggerShoe();
        TriggerLegs();
        
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

        if (other.gameObject.tag == "Looper")
        {
            //transform.rotation = new Quaternion(transform.rotation.x, -90f, transform.rotation.z, transform.rotation.w);
            transform.Rotate(new Vector3(0f, -90f, 0));
            transform.position = GameManager.instance.loopSpawn.transform.position;
        }

        if (other.gameObject.tag == "Switch")
        {
            _inSwitchTrigger = true;
            currentInteractable = other.gameObject;
        }

        if (other.gameObject.tag == "Shoe")
        {
            _inShoeTrigger = true;
            currentInteractable = other.gameObject;
        }

        if (other.gameObject.tag == "Legs")
        {
            _inLegsTrigger = true;
            currentInteractable = other.gameObject;
        }

        if (other.gameObject.tag == "Dove")
        {
            GameManager.instance.blackPuzzlesCompleted++;
            doveCollected = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "DoveIcon")
        {
            _inDoveTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DoveIcon")
        {
            _inDoveTrigger = true;
        }
    }


    void TriggerSwitch()
    {
        if (!_inSwitchTrigger)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            Switch switchComp = currentInteractable.GetComponent<Switch>();
            switchComp.isEnabled = !switchComp.isEnabled;
        }
    }

    void TriggerShoe()
    {
        if (!_inShoeTrigger)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (currentInteractable.GetComponent<Coffin>() == null)
            {
                _inShoeTrigger = false;
            }

            if (currentInteractable.GetComponent<Coffin>().isSolved)
            {
                shoesCollected++;
                GameManager.instance.isHallwayTriggerActive = false;
                currentInteractable.gameObject.SetActive(false);
            }
        }
    }

    void TriggerLegs()
    {
        if (!_inLegsTrigger)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            Legs legComp = currentInteractable.GetComponent<Legs>();

            if (shoesCollected == 1 && !legComp.hasLeftShoe)
            {
                legComp.hasLeftShoe = true;
            }

            if (shoesCollected >= 2 && !legComp.hasRightShoe)
            {
                legComp.hasRightShoe = true;
            }
        }
    }

    void TriggerDoveIcon()
    {
        if (!_inDoveTrigger || !doveCollected)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            GameManager.instance.isDoveIconActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Switch")
        {
            _inSwitchTrigger = false;
            currentInteractable = null;
        }

        if (other.gameObject.tag == "Shoe")
        {
            _inShoeTrigger = false;
            currentInteractable = null;
        }

        if (other.gameObject.tag == "Legs")
        {
            _inLegsTrigger = false;
            currentInteractable = null;
        }

        if (other.gameObject.tag == "DoveIcon")
        {
            _inDoveTrigger = false;
        }
    }
}
