using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public GameObject leftShoe;
    public GameObject rightShoe;

    public bool hasLeftShoe = false;
    public bool hasRightShoe = false;

    public bool puzzleComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        leftShoe.SetActive(false);
        rightShoe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        leftShoe.SetActive(hasLeftShoe);
        rightShoe.SetActive(hasRightShoe);

        puzzleComplete = hasLeftShoe && hasRightShoe;

        if (puzzleComplete && GameManager.instance.whitePuzzlesCompleted != 2)
        {
            GameManager.instance.whitePuzzlesCompleted = 2;
        }
        
    }
}
