using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.persistentDataPath + "/saveData.dat";
        gameManager = FindObjectOfType<GameManager>();
    }


    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream file = File.Create(savePath))
            {
                SaveData data = new SaveData();
                data.Keys = gameManager.playerInventory.GetKeys();
                data.x = gameManager.lastPlayerPosition.x;
                data.y = gameManager.lastPlayerPosition.y;
                data.z = gameManager.lastPlayerPosition.z;
                data.savedLevel = gameManager.currentLevel;
                data.hasLighter = gameManager.playerInventory.lighter.hasLighter;

                bf.Serialize(file, data);
            }
            Debug.Log("Data saved successfully to: " + savePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save data: " + e.Message);
        }

    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogError("Save file not found.");
            return;
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Open(savePath, FileMode.Open))
            {
                if (file.Length > 0)
                {
                    SaveData data = (SaveData)bf.Deserialize(file);

                    if (gameManager == null)
                    {
                        Debug.LogError("GameManager not found.");
                    }

                    gameManager.currentLevel = data.savedLevel;
                    gameManager.lastPlayerPosition = new Vector3(data.x, data.y, data.z);
                    Debug.Log("Player position loaded: " + gameManager.lastPlayerPosition);

                    if (gameManager.playerInventory == null)
                    {
                        Debug.LogError("PlayerInventory not found.");
                    }

                    foreach (int key in data.Keys)
                    {
                        gameManager.playerInventory.AddKey(key);
                    }
                    gameManager.playerInventory.lighter.hasLighter = data.hasLighter;
                }
            }
            Debug.Log("Data loaded successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load data from file: " + e.Message);
        }

    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.LogWarning("No save file found to delete.");
        }

        gameManager.ResetValues();
    }
}
