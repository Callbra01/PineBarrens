using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    Quaternion stopQ = new Quaternion(0f, 50.2005f, 0f, 0f);
    Quaternion goQ = new Quaternion(0f, 230.6623f, 0f, 0f);

    public Transform stopTransform;
    public Transform goTransform;

    public bool showGo = false;

    void Start()
    {
        this.transform.rotation = stopQ;
    }

    void Update()
    {
        if (showGo)
        {
            if (transform.rotation != goTransform.rotation)
            {
                transform.rotation = goTransform.rotation;
            }
        }
        else
        {
            if (transform.rotation != stopTransform.rotation)
            {
                transform.rotation = stopTransform.rotation;
            }
        }
    }
}
