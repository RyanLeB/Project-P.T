using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool isInteractable;

    /// <summary>
    /// The type of interactable object.
    /// </summary>
    public enum InteractableType
    {
        Key,
        Lighter,
        LighterFluid,
        Door,
        KeyPad,
        Lightswitch,
        TVRemote
    }

    public enum InteractionType
    {
        Pickup,
        Use,
        Open,
        Close,
        TurnOn,
        TurnOff
    }

    public InteractableType type;
    public InteractionType interactionType;
}
