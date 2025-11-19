using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isEnabled = false;
    public GameObject switchLight;



    // Update is called once per frame
    void Update()
    {
        switchLight.SetActive(isEnabled);

        if (isEnabled)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
