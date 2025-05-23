using UnityEngine;
using TMPro;

public class JoinGameUI : PanelBase
{
    public TMP_InputField joinGameIdInput;
    public GameObject playerPanel;

    public void OnJoinGameConfirm()
    {
        GameData dummyGame = new GameData
        {
            gameId = joinGameIdInput.text,
            gameName = "Loaded Game " + joinGameIdInput.text,
            gmUserIds = new(),
            playerUserIds = new(),
            characters = new()
        };
        GameManager.Instance.JoinGame(dummyGame);

        gameObject.SetActive(false);
        playerPanel.SetActive(true);
    }

    public void Refresh()
    {
        // maybe reload available games to join if you add that later
    }
}
