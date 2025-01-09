using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevelChanging : MonoBehaviour
{
    public GameObject[] levels;
    public int currentLevel = 0;
    
    private Animator _animator;
    
    void Start()
    {
        levels[currentLevel].SetActive(true);
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator
            levels[currentLevel].SetActive(false);
            currentLevel++;
            if (currentLevel >= levels.Length)
            {
                currentLevel = 0;
            }
            levels[currentLevel].SetActive(true);
        }
    }
}
