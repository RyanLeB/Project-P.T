using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public float maxIntensity = 1.1f;

    public float lighterFluid = 100f; // 100% full

    public bool hasLighter = false;

    [SerializeField] private Light lighterLight;

    public void Start()
    {
        lighterLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lighterFluid >= 110)
        {
            lighterLight.intensity = maxIntensity;
        }
        else
        {
            lighterLight.intensity = lighterFluid / 100;
        }
    }

    public void CheckLighter()
    {
        switch (hasLighter)
        {
            case true:
                gameObject.SetActive(true);
                lighterFluid -= Time.deltaTime;
                break;
            case false:
                gameObject.SetActive(false);
                break;
        }
    }
}
