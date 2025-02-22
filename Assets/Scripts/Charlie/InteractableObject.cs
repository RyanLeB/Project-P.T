using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class InteractableObject : MonoBehaviour
{

    public bool isInteractable = true;

    [SerializeField] private Interaction interaction;

    public GameObject triggerToActivate;

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
        TVRemote,
        KeyPiece
    }


    /// <summary>
    /// The type of interaction. Mostly used for the button prompt.
    /// </summary>
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

    private void Start()
    {
        interaction = FindObjectOfType<Interaction>();
    }

    /// <summary>
    /// Interacts with the object. The specific interaction depends on the object's type.
    /// </summary>
    public void Interact()
    {
        Debug.Log("Interacting with " + type);

        switch (type)
        {
            case InteractableType.Key:
                Key();
                break;
            case InteractableType.Lighter:
                Lighter();
                break;
            case InteractableType.LighterFluid:
                LighterFluid();
                break;
            case InteractableType.Door:
                Door();
                break;
            case InteractableType.KeyPad:
                KeyPad();
                break;
            case InteractableType.Lightswitch:
                Lightswitch();
                break;
            case InteractableType.TVRemote:
                TVRemote();
                break;
            case InteractableType.KeyPiece:
                KeyPiece();
                break;
        }

        if (triggerToActivate != null)
        {
            triggerToActivate.SetActive(true);
        }
    }

    #region Interactable Methods

    /// <summary>
    /// Adds the key to the player's inventory.
    /// </summary>
    public void Key()
    {
        Key key = GetComponent<Key>();
        interaction.inventory.GetKeys().Add(key.keyID);
        interaction.inventory.keyGameObject.SetActive(true);
        DestroyPickup();
    }

    /// <summary>
    /// Adds the lighter to the player's inventory or refills the lighter fluid if the player already has it.
    /// </summary>
    public void Lighter()
    {
        if (interaction.lighter.hasLighter == false)
        {
            interaction.lighter.hasLighter = true;
        }
        else if (interaction.lighter.hasLighter == true)
        {
            interaction.lighter.AddLighterFluid(lighterPickupIncrease);
        }
        DestroyPickup();
    }

    /// <summary>
    /// Refills the lighter fluid.
    /// </summary>
    public void LighterFluid()
    {
        interaction.lighter.AddLighterFluid(lighterFluidPickupIncrease);
        DestroyPickup();
    }


    /// <summary>
    /// Tries to open the door.
    /// </summary>
    public void Door()
    {
        Door door = GetComponent<Door>();
        door.TryOpen();
    }

    /// <summary>
    /// Opens the keypad UI.
    /// </summary>
    public void KeyPad()
    {
        this.isInteractable = false;
        interaction.keypadUI.SetActive(true);
        interaction.gameManager.ChangeGameState(GameManager.GameState.KeyPad);
        interaction.codeLock.currentKeypad = GetComponent<Keypad>();
    }

    /// <summary>
    /// Toggles the lightswitch.
    /// </summary>
    public void Lightswitch()
    {
        Lightswitch lightswitch = GetComponent<Lightswitch>();
        lightswitch.ToggleSwitch();
    }

    /// <summary>
    /// Toggles the TV remote.
    /// </summary>
    public void TVRemote()
    {
        Debug.Log("TV Remote flicked");
    }
    #endregion

    /// <summary>
    /// Destroys the pickup object. Disables the object and the interact text.
    /// </summary>
    void DestroyPickup()
    {
        gameObject.SetActive(false);
        //interaction.interactableFinder.interactText.gameObject.SetActive(false);
    }

    void KeyPiece()
    {
        DestroyPickup();
    }

}
