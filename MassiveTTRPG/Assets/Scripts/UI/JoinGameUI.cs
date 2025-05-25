using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class JoinGameUI : PanelBase
{
    [Header("UI References")]
    public TMP_InputField filterInput;          // Text field for typing game name or ID
    public TMP_Dropdown gameSelectDropdown;     // Dropdown to pick from filtered games

    private List<string> allGameIds   = new(); // All candidate game IDs
    private List<string> allGameNames = new(); // Matching names, same order
    private List<string> filteredIds;          // Currently filtered subset
    private List<string> filteredNames;        // Currently filtered subset

    public override void RefreshPanel()
    {
        Debug.Log("this particualr panel got refreshed");
        // Load every saved game into allGameIds / allGameNames
        allGameIds.Clear();
        allGameNames.Clear();
        string dir = UnityEngine.Application.persistentDataPath;

        foreach (var file in Directory.GetFiles(dir, "game_*.json"))
        {
            var json = File.ReadAllText(file);
            var data = JsonUtility.FromJson<GameData>(json);

            // Skip games the user already belongs to
            var uid = GameManager.Instance.currentUser.userId;
            if (data.gmUserIds.Contains(uid) || data.playerUserIds.Contains(uid))
                continue;

            allGameIds.Add(data.gameId);
            allGameNames.Add(data.gameName);
        }

        // Hook up filtering
        filterInput.onValueChanged.RemoveAllListeners();
        filterInput.onValueChanged.AddListener(_ => ApplyFilter(filterInput.text));

        // Initial full list
        ApplyFilter(string.Empty);
    }

    private void ApplyFilter(string filter)
    {
        // Case-insensitive substring match on name or ID
        filteredIds   = new List<string>();
        filteredNames = new List<string>();

        string low = filter?.Trim().ToLowerInvariant() ?? "";

        for (int i = 0; i < allGameIds.Count; i++)
        {
            if (string.IsNullOrEmpty(low)
             || allGameNames[i].ToLowerInvariant().Contains(low)
             || allGameIds[i].ToLowerInvariant().Contains(low))
            {
                filteredIds.Add(allGameIds[i]);
                filteredNames.Add(allGameNames[i]);
            }
        }

        // Populate dropdown
        gameSelectDropdown.ClearOptions();
        gameSelectDropdown.AddOptions(filteredNames);
        gameSelectDropdown.interactable = filteredNames.Count > 0;
    }

    public void OnJoinGameConfirm()
    {
        if (filteredIds == null || filteredIds.Count == 0)
            return;

        // Look up the selected gameId
        string gameId = filteredIds[gameSelectDropdown.value];
        var   data   = SaveManager.LoadGame(gameId);

        // Join it and switch to player panel
        GameManager.Instance.JoinGame(data);
        NavigationManager.Instance.OpenPanel("Player - Panel");
    }
}
