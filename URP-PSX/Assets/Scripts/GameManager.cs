using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject whiteOrb;
    public GameObject blackOrb;
    public GameObject whiteOrbPivotTransform;
    public GameObject blackOrbPivotTransform;

    public GameObject ribbonObject;

    public float rotateSpeed;

    public int whitePuzzlesCompleted = 0;
    public int blackPuzzlesCompleted = 0;

    bool isWhiteOrbPlaced = false;
    bool isBlackOrbPlaced = false;

    void Start()
    {

        whiteOrb.transform.parent = whiteOrbPivotTransform.transform;
        blackOrb.transform.parent = blackOrbPivotTransform.transform;
    }

    void Update()
    {

        HandleOrbs();
        HandleRibbon();
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
            if (orb.gameObject.tag == "WhiteOrb")
                isWhiteOrbPlaced = true;

            if (orb.gameObject.tag == "BlackOrb")
                isBlackOrbPlaced = true;
        }
    }

    void HandleRibbon()
    {
        if (isWhiteOrbPlaced && isBlackOrbPlaced)
        {
            if (ribbonObject.transform.rotation.z > 0)
            {
                ribbonObject.transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
            }
        }
    }
}
