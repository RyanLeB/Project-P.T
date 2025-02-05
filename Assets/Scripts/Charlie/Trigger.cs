using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool soundTrigger;
    public bool doorTrigger;

    public Trigger nextTrigger;

    [Header("Sound Trigger Settings")]

    public GameObject soundPosition;

    public AudioClip sound;

    public bool playOnStart = false;

    public bool loop = false;

    public float spacialBlend = 1;

    public float volume = 1;


    private AudioSource audioSource;

    [Header("Door Trigger Settings")]

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

    public void Start()
    {
        if (soundTrigger && soundPosition == null)
        {
            Debug.Assert(soundPosition != null, "Sound Position is null");
        }

        if (soundTrigger)
        {
            audioSource = soundPosition.AddComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.playOnAwake = playOnStart;
            audioSource.loop = loop;
            audioSource.spatialBlend = spacialBlend;
            audioSource.volume = volume;
        }

        if (nextTrigger != null)
        {
            nextTrigger.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (soundTrigger)
        {
            audioSource.Play();
        }
        if (doorTrigger)
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
        }

        if (nextTrigger != null)
        {
            nextTrigger.gameObject.SetActive(true);
        }

        Destroy(this);
    }
}

//[CustomEditor(typeof(Trigger))]
//public class TriggerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        Trigger trigger = (Trigger)target;

//        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundTrigger"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("doorTrigger"));

//        EditorGUILayout.PropertyField(serializedObject.FindProperty("nextTrigger"));

//        if (trigger.soundTrigger)
//        {
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundPosition"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("sound"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnStart"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("spacialBlend"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("volume"));
//        }
//        if (trigger.doorTrigger)
//        {
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("door"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionType"));
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
