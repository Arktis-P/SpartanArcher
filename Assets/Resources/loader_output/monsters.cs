using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class monsters
{
    /// <summary>
    /// id
    /// </summary>
    public int key;

    /// <summary>
    /// monster's type
    /// </summary>
    public DesignEnums.MonsterType type;

    /// <summary>
    /// monster's name
    /// </summary>
    public string name;

    /// <summary>
    /// health
    /// </summary>
    public float health;

    /// <summary>
    /// maxhealth
    /// </summary>
    public float maxhealth;

    /// <summary>
    /// monster's moving speed
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// monster's attack frequencty
    /// </summary>
    public float attackFreq;

    /// <summary>
    /// monster's detection range for player
    /// </summary>
    public float detectionRange;

    /// <summary>
    /// monster's shooting range
    /// </summary>
    public float shootingRange;

}
public class monstersLoader
{
    public List<monsters> ItemsList { get; private set; }
    public Dictionary<int, monsters> ItemsDict { get; private set; }

    public monstersLoader(string path = "JSON/monsters")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, monsters>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<monsters> Items;
    }

    public monsters GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public monsters GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
