using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Rebind : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;

    [SerializeField] private TextMeshProUGUI rebindText;

    [SerializeField] private TextMeshProUGUI label;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public string defaultBinding;

    public int currentBindingIndex = 0;

    private void Awake()
    {
        defaultBinding = actionReference.action.bindings[currentBindingIndex].effectivePath;
    }

    private void Update()
    {
        if (label != null)
        {
            label.text = actionReference.action.name + " " + (actionReference.action.bindings[currentBindingIndex].name);
        }

        if (rebindText != null)
        {
            rebindText.text = InputControlPath.ToHumanReadableString(actionReference.action.bindings[currentBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }

    public void StartRebind()
    {
        EventSystem.current.SetSelectedGameObject(null);

        rebindText.text = "Press a key...";

        actionReference.action.Disable();


        rebindOperation = actionReference.action.PerformInteractiveRebinding(currentBindingIndex)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    void RebindComplete()
    {
        rebindText.text = InputControlPath.ToHumanReadableString(actionReference.action.bindings[currentBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        rebindOperation.Dispose();
        actionReference.action.Enable();
    }

    public void ResetBinding()
    {
        Debug.Log("Resetting binding to " + defaultBinding);
        actionReference.action.Disable();
        actionReference.action.RemoveBindingOverride(currentBindingIndex);
        actionReference.action.Enable();
        rebindText.text = InputControlPath.ToHumanReadableString(actionReference.action.bindings[currentBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
