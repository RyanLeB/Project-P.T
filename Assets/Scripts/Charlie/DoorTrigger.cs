using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    public enum DoorActionType
    {
        Open,
        Close,
        Unlock,
        Lock,
        Crack
    }

    public DoorActionType actionType;

    private void OnTriggerEnter(Collider other)
    {
        switch (actionType)
        {
            case DoorActionType.Open:
                door.Open();
                break;
            case DoorActionType.Close:
                door.Close();
                break;
            case DoorActionType.Unlock:
                door.UnlockDoor();
                break;
            case DoorActionType.Lock:
                door.Lock();
                break;
            case DoorActionType.Crack:
                door.Crack();
                break;
        }

        Destroy(this);
    }
}
