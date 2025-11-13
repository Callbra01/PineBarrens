using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] idleFrames;

    [SerializeField]
    private Sprite[] pointFrames;

    [SerializeField]
    private Sprite[] flippingFrames;

    public int idleSpeed = 51;
    public int pointSpeed = 24;
    int timer = 0;
    int currentFrame = 0;

    private Image image;

    bool isPointing = false;
    bool isFlipping = false;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPointing && !isFlipping)
        {
            currentFrame = 0;
            isPointing = true;
        }

        if (Input.GetMouseButtonDown(1) && !isPointing && !isFlipping)
        {
            //currentFrame = 0;
            //isFlipping = true;
        }

        if (isPointing)
        {
            HandlePointing();
        }
        else if (isFlipping)
        {
            HandleFlipping();
        }
        else
        {
            HandleIdle();
        }
        
    }

    void HandlePointing()
    {
        image.sprite = pointFrames[currentFrame];

        if (timer % pointSpeed == 0)
        {
            currentFrame++;
        }

        if (currentFrame >= pointFrames.Length)
        {
            timer = 0;
            currentFrame = 0;
            isPointing = false;
        }

        timer++;

    }

    void HandleFlipping()
    {
        image.sprite = flippingFrames[currentFrame];

        if (timer % pointSpeed == 0)
        {
            currentFrame++;
        }

        if (currentFrame >= flippingFrames.Length)
        {
            timer = 0;
            currentFrame = 0;
            isFlipping = false;
        }

        timer++;
    }

    void HandleIdle()
    {
        if (timer % idleSpeed == 0)
        {
            currentFrame++;
        }

        if (currentFrame >= idleFrames.Length)
        {
            timer = 0;
            currentFrame = 0;

        }

        timer++;
        image.sprite = idleFrames[currentFrame];
    }

    void HandleTimer(int frameCount)
    {

    }

}
