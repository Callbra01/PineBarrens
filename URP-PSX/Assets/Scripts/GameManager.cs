using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public GameObject whiteOrb;
    public GameObject blackOrb;
    public GameObject whiteOrbPivotTransform;
    public GameObject blackOrbPivotTransform;
    public Light[] whiteLights = new Light[2];
    public Light[] blackLights = new Light[2];

    public GameObject ribbonObject;
    public GameObject doorPiece;


    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftDoorPivot;
    public GameObject rightDoorPivot;

    public GameObject canvasHand;

    public float rotateSpeed;

    public int whitePuzzlesCompleted = 0;
    public int blackPuzzlesCompleted = 0;

    bool isWhiteOrbPlaced = false;
    bool isBlackOrbPlaced = false;

    public bool isHandVisible = true;


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
        canvasHand = GameObject.FindGameObjectWithTag("HandOverlay");

        whiteOrb.transform.parent = whiteOrbPivotTransform.transform;
        blackOrb.transform.parent = blackOrbPivotTransform.transform;

        leftDoor.transform.parent = leftDoorPivot.transform;
        rightDoor.transform.parent = rightDoorPivot.transform;

        doorPiece.GetComponent<Rigidbody>().useGravity = false;
    }

    void Update()
    {
        canvasHand.SetActive(isHandVisible);

        HandleLights();
        HandleOrbs();
        HandleRibbon();
        HandleDoorPiece();
    }

    void HandleDoorPiece()
    {
        if (doorPiece.transform.position.y <= -3.6f)
        {
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        if (leftDoorPivot.transform.rotation.y >= -0.626)
            leftDoorPivot.transform.Rotate(new Vector3(0, -15f * Time.deltaTime, 0));

        if (rightDoorPivot.transform.rotation.y <= 0.626)
            rightDoorPivot.transform.Rotate(new Vector3(0, 15f * Time.deltaTime, 0));
    }

    void HandleLights()
    {
        if (whitePuzzlesCompleted == 1)
        {
            whiteLights[0].enabled = true;
        }
        else if (whitePuzzlesCompleted == 2)
        {
            whiteLights[1].enabled = true;
        }

        if (blackPuzzlesCompleted == 1)
        {
            blackLights[0].enabled = true;
        }
        else if (blackPuzzlesCompleted == 2)
        {
            blackLights[1].enabled = true;
        }

    }


    void HandleOrbs()
    {
        if (whitePuzzlesCompleted == 2)
        {
            HandleOrb(whiteOrbPivotTransform);
        }

        if (blackPuzzlesCompleted == 2)
        {
            HandleOrb(blackOrbPivotTransform);
        }
    }

    void HandleOrb(GameObject orb)
    {
        if (orb.transform.rotation.z < 0.7)
        {
            orb.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
        else
        {
            orb.transform.position -= new Vector3(0, 0, 1 * Time.deltaTime);

            if (orb.transform.position.z <= -38)
            {
                if (orb.gameObject.tag == "WhiteOrb")
                    isWhiteOrbPlaced = true;

                if (orb.gameObject.tag == "BlackOrb")
                    isBlackOrbPlaced = true;

                orb.gameObject.SetActive(false);
            }
        }
    }

    void HandleRibbon()
    {
        if (isWhiteOrbPlaced && isBlackOrbPlaced)
        {
            if (ribbonObject.transform.rotation.z > 0)
            {
                ribbonObject.transform.Rotate(new Vector3(0, 0, -rotateSpeed * 2f * Time.deltaTime));
            }
            else
            {
                doorPiece.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
