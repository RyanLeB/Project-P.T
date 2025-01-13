using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonNoah : MonoBehaviour
{
    private static SingletonNoah instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
