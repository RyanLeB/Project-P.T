using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;



public class SteamIntegration : MonoBehaviour
{
  public void WinAchievement()
  {
    if (SteamManager.Initialized)
    {
      // Check if the user has already achieved the achievement
      bool isAchieved = SteamUserStats.GetAchievement("WIN_ACHIEVE", out bool achieved);
      if (!achieved) // Corrected condition to check if the achievement is not yet unlocked
      {
        SteamUserStats.SetAchievement("WIN_ACHIEVE");
        SteamUserStats.StoreStats();
      }
      else
      {
        Debug.Log("Achievement already unlocked.");
      }
    }

  }
}
    

