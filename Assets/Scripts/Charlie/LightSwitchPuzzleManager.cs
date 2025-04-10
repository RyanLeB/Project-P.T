using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchPuzzleManager : MonoBehaviour
{
    public GameObject[] lights;

    public bool puzzleSolved = false;

    public int numberOfLightsOn = 0;

    public int numberOfLightsTotal;

    public GameObject cageObject;
    public GameObject keySpawnPoint;

    public Door doorToOpen;

    public void TurnOnLight(int index)
    {
        lights[index].SetActive(true);
        numberOfLightsOn++;
    }

    public void TurnOffLight(int index)
    {
        lights[index].SetActive(false);
        numberOfLightsOn--;
    }

    public void Update()
    {
        if (numberOfLightsOn == numberOfLightsTotal)
        {
            puzzleSolved = true;
        }

        if (puzzleSolved)
        {
            if (cageObject != null)
            {
                cageObject.SetActive(false);
            }
            if (doorToOpen != null)
            {
                doorToOpen.UnlockDoor();
            }
            if (keySpawnPoint != null)
            {
                keySpawnPoint.SetActive(true);
            }
        }
    }
}
