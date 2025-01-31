using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    // Player Variables

    public const float defaultSpeed = 5f;
    public const float defaultHeight = 2f;

    public const float cameraVerticalRotationLimit = 65f;
    public const float cameraSensitivity = 0.15f;

    public const float gravity = -9.81f;
    public const float distance = 1.05f;

    public const float runningSpeedMultiplier = 2f;
    public const float crouchingSpeedMultiplier = 0.4f;
    public const float stairsSpeedMultiplier = 0.5f;

    public const float crouchingHeightMultiplier = 0.25f;

    // Lighter Variables

    public const float maxIntensity = 1.1f;
    public const float minIntensity = 0.1f;

    public const float maxLighterFluid = 100;

    public const float startingLighterFluid = 100f;

    public const float lighterFluidDecreaseRate = 0.5f;


    public const float lighterPickupIncrease = 100f;
    public const float lighterFluidPickupIncrease = 50f;

    // Keypad Variables

    public const int defaultPasswordLength = 4;

    // Lightswitch Variables

    public const int defaultStatesToRemember = 5;
}
