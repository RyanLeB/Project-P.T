using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Coroutine quit = StartCoroutine(QuitGame());
    }


    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(8);
        Application.Quit();
    }
}
