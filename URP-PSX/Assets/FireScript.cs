using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    public static FireScript instance { get; private set; }

    Light lightComp;

    float newIntensity = 0.18f;
    public float rateOfChange = 1f;

    bool add = true;

    public Image blackOverlay;

    public bool toggleOverlay = false;

    Color overlayEnd = new Color(0, 0, 0, 100);

    Color overlayEndTransparent = new Color(0, 0, 0, 0);

    public bool moveToScene = true;

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

    // Start is called before the first frame update
    void Start()
    {
        lightComp = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightComp.intensity = newIntensity;

        if (add)
        {
            newIntensity += rateOfChange * Time.deltaTime;
        }
        else
        {
            newIntensity -= rateOfChange * Time.deltaTime;
        }

        if (newIntensity <= 0.18f)
        {
            add = true;
        }
        else if (newIntensity >= 0.96f)
        {
            add = false;
        }

        if (toggleOverlay)
        {
            if (moveToScene)
            {
                if (blackOverlay.color.a < 100)
                {
                    blackOverlay.color = Color.Lerp(blackOverlay.color, overlayEnd, 0.05f * Time.deltaTime);
                }


                if (blackOverlay.color.a >= 10)
                {
                    if (moveToScene)
                        SceneManager.LoadScene("Level Design");
                }
            }
            else
            {
                if (blackOverlay.color.a > 0)
                {
                    blackOverlay.color = Color.Lerp(blackOverlay.color, overlayEndTransparent, 0.05f * Time.deltaTime);
                }
            }
        }

    }
}
