using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelUI : MonoBehaviour
{
    public Transform publicListParent;
    public Transform assignedListParent;
    public GameObject characterCardPrefab;
    public CharacterDetailUI detailPanel;

    private void OnEnable()
    {
        RefreshLists();
    }

    public void RefreshLists()
    {
        foreach (Transform child in publicListParent)
            Destroy(child.gameObject);
        foreach (Transform child in assignedListParent)
            Destroy(child.gameObject);

        var characters = GameManager.Instance.currentGame.characters;
        var userId = GameManager.Instance.currentUser.userId;

        foreach (var character in characters)
        {
            if (character.assignedToPlayerId == userId)
            {
                var card = Instantiate(characterCardPrefab, assignedListParent);
                card.GetComponent<CharacterDisplay>().Setup(character, (data) => {
                    detailPanel.Open(data, false);
                });
            }
            else if (character.isPublic)
            {
                var card = Instantiate(characterCardPrefab, publicListParent);
                card.GetComponent<CharacterDisplay>().Setup(character, (data) => {
                    detailPanel.Open(data, false);
                });
            }
        }
    }
}
