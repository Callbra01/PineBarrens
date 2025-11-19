using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    public GameObject coffinLid;
    public GameObject coffinLight;
    public bool isSolved = false;

    public GameObject shoe;
    public bool isShoeCollected = false;

    public int shoeNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        coffinLid.SetActive(true);
        coffinLight.SetActive(false);
        if (shoe != null )
            shoe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coffinLid.SetActive(!isSolved);
        coffinLight.SetActive(isSolved);
        if (shoe != null)
            shoe.SetActive(isSolved);
    }

}
