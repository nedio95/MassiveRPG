using UnityEngine;
using TMPro;

public class CreateGameUI : PanelBase
{
    public TMP_InputField gameNameInput;
    public GameObject gmPanel;

    public void OnStartGamePressed()
    {
        GameManager.Instance.CreateGame(gameNameInput.text);
        gameObject.SetActive(false);
        gmPanel.SetActive(true);
    }

    public override void RefreshPanel()
    {
        // auto-fill a new suggested game name
    }
}
