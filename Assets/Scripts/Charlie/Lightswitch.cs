using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static GlobalVariables;

public class Lightswitch : MonoBehaviour
{

    /// <summary>
    /// The type of lightswitch.
    /// </summary>
    public enum LightswitchType
    {
        LightswitchPuzzle,
        Trigger
    }

    public LightswitchType lightswitchType;

    public Animator animator;

    [SerializeField] private bool lightOn;
    public GameObject[] lightObjects;
    public GameObject[] lights;

    public int lastStatesToRemember = defaultStatesToRemember;
    public List<bool> lastStates = new List<bool>();
    public List<bool> requiredLastStates = new List<bool>();

    public bool triggerActivated = false;
    public bool stateToActivate = true; // this will be the state the lightswitch will have to be for something else to happen

    /// <summary>
    /// Toggles the lightswitch on or off.
    /// </summary>
    public void ToggleSwitch()
    {
        lightOn = !lightOn;

        animator.Play("LightSwitch_" + (lightOn ? "ON" : "OFF"));

        if (lastStates.Count >= lastStatesToRemember)
        {
            lastStates.RemoveAt(0);
        }
        lastStates.Add(lightOn);
    }

    /// <summary>
    /// Something happens when the lightswitch is activated.
    /// </summary>
    public void ThingHappens()
    {
        Debug.Log("Something happens");
    }

    private void Start()
    {
        lastStates.Add(lightOn);
    }

    void Update()
    {
        foreach (GameObject lightObject in lightObjects)
        {
            Renderer renderer = lightObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;
                if (lightOn)
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

        foreach (GameObject light in lights)
        {
            light.SetActive(lightOn);
        }

        switch (lightswitchType)
        {
            case LightswitchType.LightswitchPuzzle:
                CheckLightswitchPuzzle();
                break;
            case LightswitchType.Trigger:
                CheckTrigger();
                break;
        }
    }

    /// <summary>
    /// Checks if the lightswitch puzzle has been solved.
    /// </summary>
    public void CheckLightswitchPuzzle()
    {
        if (lastStates.SequenceEqual(requiredLastStates) && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }

    /// <summary>
    /// Checks if the lightswitch has been activated.
    /// </summary>
    public void CheckTrigger()
    {
        if (lightOn == stateToActivate && !triggerActivated)
        {
            ThingHappens();
            triggerActivated = true;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Lightswitch))]
public class LightswitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Lightswitch lightSwitch = (Lightswitch)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightswitchType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightOn"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lightObjects"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lights"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("animator"));

        if (lightSwitch.lightswitchType == Lightswitch.LightswitchType.Trigger)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stateToActivate"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerActivated"));
        }
        if (lightSwitch.lightswitchType == Lightswitch.LightswitchType.LightswitchPuzzle)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lastStatesToRemember"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lastStates"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredLastStates"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
