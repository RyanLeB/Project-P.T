using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int savedLevel;

    public bool hasLighter;

    public float x;
    public float y;
    public float z;

    public List<int> Keys { get; set; }
}
