using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignCharacterUI : PanelBase
{
    public TMP_Dropdown playerDropdown;
    public TMP_Text characterNameText;
    private CharacterData currentCharacter;
    private List<string> userIdList = new();

    public override void RefreshPanel()
    {
        gameObject.SetActive(true);
    }

    public void Open(CharacterData character)
    {
        currentCharacter = character;
        characterNameText.text = $"Assign: {character.name}";

        playerDropdown.ClearOptions();
        userIdList.Clear();

        var game = GameManager.Instance.currentGame;
        foreach (var playerId in game.playerUserIds)
        {
            // skip already assigned players if you like:
            if (character.assignedToPlayerId.Contains(playerId))
                continue;

            string displayName = game.userNames[playerId];
            playerDropdown.options.Add(new TMP_Dropdown.OptionData(displayName));
            userIdList.Add(playerId);
        }

        playerDropdown.RefreshShownValue();
        RefreshPanel();
    }

    public void OnAssign()
    {
        if (userIdList.Count == 0) return;

        string selectedId = userIdList[playerDropdown.value];
        if (!currentCharacter.assignedToPlayerId.Contains(selectedId))
            currentCharacter.assignedToPlayerId.Add(selectedId);

        GameManager.Instance.SaveCurrentState();
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
