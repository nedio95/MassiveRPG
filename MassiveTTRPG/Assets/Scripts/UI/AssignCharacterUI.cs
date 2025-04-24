using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignCharacterUI : MonoBehaviour
{
    public TMP_Dropdown playerDropdown;
    public TMP_Text characterNameText;
    private CharacterData currentCharacter;
    private List<string> userIdList = new();

    public void Open(CharacterData character)
    {
        currentCharacter = character;
        characterNameText.text = $"Assign: {character.name}";
        playerDropdown.ClearOptions();
        userIdList.Clear();

        var userNames = GameManager.Instance.currentGame.userNames;

        foreach (string playerId in GameManager.Instance.currentGame.playerUserIds)
        {
            string displayName = userNames.ContainsKey(playerId) ? userNames[playerId] : playerId;
            playerDropdown.options.Add(new TMP_Dropdown.OptionData(displayName));
            userIdList.Add(playerId); // Maintain order
        }

        playerDropdown.RefreshShownValue();
        gameObject.SetActive(true);
    }

    public void OnAssign()
    {
        if (userIdList.Count > 0)
        {
            string selectedId = userIdList[playerDropdown.value];
            currentCharacter.assignedToPlayerId = selectedId;
            GameManager.Instance.SaveCurrentState();
        }

        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
