using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public enum InteractableType
    {
        Key,
        Lighter,
        LighterFluid,
        Door,
        KeyPad
    }

    public InteractableType type;
}
