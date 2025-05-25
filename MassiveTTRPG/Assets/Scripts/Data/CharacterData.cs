using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string id;
    public string name;
    public string role;
    public string public_description;
    public string private_description;
    public bool isPublic;
    public List<string> assignedToPlayerId = new();

    public string artworkUrl;
}
