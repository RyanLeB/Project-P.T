using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
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

    public InteractableType type;
}
