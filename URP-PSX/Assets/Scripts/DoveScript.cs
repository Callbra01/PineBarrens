using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoveScript : MonoBehaviour
{
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 57.26f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            this.transform.Rotate(0, 15f * Time.deltaTime, 0);

            if (transform.position.y > 5)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (5f * Time.deltaTime), transform.position.z);
            }
        }
    }
}
