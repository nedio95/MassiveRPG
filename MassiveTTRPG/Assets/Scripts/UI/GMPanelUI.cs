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
        NavigationManager.Instance.OpenPanel("Create Character - Panel");
    }
}
