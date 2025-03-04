using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int savedLevel;

    public bool hasLighter;

    public List<int> Keys { get; set; }
}
