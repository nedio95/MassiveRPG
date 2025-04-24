using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string gameId;
    public string gameName;
    public List<string> gmUserIds = new();
    public List<string> playerUserIds = new();
    public List<CharacterData> characters = new();

    public Dictionary<string, string> userNames = new();
}

