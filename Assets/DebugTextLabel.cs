using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTextLabel : MonoBehaviour
{
    public static DebugTextLabel instance;

    public void Awake()
    {
        Application.RegisterLogCallback(ReceivedLog);
    }

    private void ReceivedLog(string condition, string stackTrace, LogType type)
    {
        text += $"{condition}\n";
        if (type == LogType.Exception)
        {
            text += $"{stackTrace}\n";
        }
    }
    public static string text
    {
        get => instance != null ? instance.textLabel.text : "";
        set
        {
            if (instance != null)
            {
                instance.textLabel.text = value;
            }
            if (value != "")
                Debug.Log(value);
        }
    }
    public TMPro.TextMeshProUGUI textLabel;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out textLabel);
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
