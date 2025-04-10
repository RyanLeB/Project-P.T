using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool soundTrigger;
    public bool doorTrigger;
    public bool lightTrigger;

    public Trigger nextTrigger;

    public GameObject objectToDestroy;

    public GameObject objectToSpawn; // ill probably change this to an animation later

    public List<GameObject> objectsToDestroy;

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

    [Header("Light Trigger Settings")]

    public bool lightOnOff = false; // if true, the lights will turn on, if false, the lights will turn off
    public GameObject[] lights;
    public GameObject[] lightObjects;

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
        if (other.tag != "Player")
        {
            return;
        }
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

        if (lightTrigger)
        {
            TriggerLights();
        }

        if (nextTrigger != null)
        {
            nextTrigger.gameObject.SetActive(true);
        }

        if (objectToDestroy != null)
        {
            objectToDestroy.SetActive(false);
        }

        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(true);
        }

        if (objectsToDestroy.Count > 0)
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                obj.SetActive(false);
            }
        }

        Destroy(this);
    }

    private void TriggerLights()
    {
        if (lights != null)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(lightOnOff);
            }
        }
        foreach (GameObject lightObject in lightObjects)
        {
            Renderer renderer = lightObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;
                if (lightOnOff)
                {
                    material.EnableKeyword("_EMISSION");
                    //material.SetColor("_EmissionColor", new Color(207, 140, 43));
                }
                else
                {
                    material.DisableKeyword("_EMISSION");
                    //material.SetColor("_EmissionColor", Color.black);
                }
            }
            // lightObject.GetComponent<Light>().color = new Color(-1f, -1f, -1f); if this is on the light will remove light from the area. could be used for something cool?
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Trigger trigger = (Trigger)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundTrigger"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("doorTrigger"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightTrigger"));


        EditorGUILayout.PropertyField(serializedObject.FindProperty("objectToSpawn"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("objectToDestroy"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("objectsToDestroy"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nextTrigger"));

        if (trigger.soundTrigger)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundPosition"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sound"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playOnStart"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spacialBlend"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("volume"));
        }
        if (trigger.doorTrigger)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("door"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionType"));
        }

        if (trigger.lightTrigger)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lights"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lightObjects"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
