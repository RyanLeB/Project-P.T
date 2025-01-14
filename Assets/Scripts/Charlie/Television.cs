using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : MonoBehaviour
{
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleTV()
    {
        isOn = !isOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            // Debug.Log("Television is on");
            // play sound or whatever
        }
    }
}
