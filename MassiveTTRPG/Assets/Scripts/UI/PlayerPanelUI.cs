using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelUI : PanelBase
{
    [Header("UI References")]
    public Transform publicListParent;
    public Transform assignedListParent;
    public GameObject characterCardPrefab;
    public CharacterDetailUI detailPanel;

    public override void RefreshPanel()
    {
        RefreshLists();
    }

    private void RefreshLists()
    {
        // Clear old entries
        foreach (Transform t in publicListParent) Destroy(t.gameObject);
        foreach (Transform t in assignedListParent) Destroy(t.gameObject);

        var game = GameManager.Instance.currentGame;
        var userId = GameManager.Instance.currentUser.userId;

        foreach (var character in game.characters)
        {
            // now using the List<string>
            if (character.assignedToPlayerId.Contains(userId))
            {
                var go = Instantiate(characterCardPrefab, assignedListParent);
                go.GetComponent<CharacterDisplay>()
                  .Setup(character, data => detailPanel.Open(data, false));
            }
            else if (character.isPublic)
            {
                var go = Instantiate(characterCardPrefab, publicListParent);
                go.GetComponent<CharacterDisplay>()
                  .Setup(character, data => detailPanel.Open(data, false));
            }
        }
    }
}
