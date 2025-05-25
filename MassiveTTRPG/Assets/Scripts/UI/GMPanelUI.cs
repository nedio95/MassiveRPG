using System.Collections.Generic;
using UnityEngine;

public class GMPanelUI : PanelBase
{
    public Transform listParent;
    public GameObject characterCardPrefab;
    public CharacterDetailUI detailPanel;
    public AssignCharacterUI assignPanel;

    public override void RefreshPanel()
    {
        // Update Main Menu stuff here
    }

    private void OnEnable()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        foreach (Transform child in listParent)
            Destroy(child.gameObject);

        List<CharacterData> characters = GameManager.Instance.currentGame.characters;

        foreach (var character in characters)
        {
            var card = Instantiate(characterCardPrefab, listParent);
            card.GetComponent<CharacterDisplay>().Setup(character,
                onView: (data) => { detailPanel.Open(data, true); },
                onAssign: (data) => { assignPanel.Open(data); 
        });
        }
    }

    public void OnCreateCharacter()
    {
        CharacterData newChar = new CharacterData
        {
            id = System.Guid.NewGuid().ToString(),
            name = "New Character",
            role = "Undefined",
            public_description = "",
            private_description = "",
            isPublic = true,
            assignedToPlayerId = null
        };

        GameManager.Instance.currentGame.characters.Add(newChar);
        RefreshList();
    }
}
