using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.IO;

public class MainMenuUI : PanelBase
{
    public TMP_InputField usernameInput;
    public GameObject 
        mainMenuPanel, 
        createGamePanel, 
        joinGamePanel, 
        continueButton, 
        continuePanel, 
        playerPanel, 
        gmPanel;
    public TMP_Dropdown gameSelectDropdown;
    private List<string> loadedGameIds = new();

    public override void RefreshPanel()
    {
        // Update Main Menu stuff here
    }

    private void Start()
    {
        Debug.Log(UnityEngine.Application.persistentDataPath);

        var savedUser = GameManager.Instance.currentUser;

        Button btn = continueButton.GetComponent<Button>();
        TMP_Text text = continueButton.GetComponentInChildren<TMP_Text>();

        if (savedUser != null)
        {
            usernameInput.text = savedUser.username;

            List<string> validGameNames = new List<string>();
            loadedGameIds.Clear();

            foreach (var gameId in savedUser.gameIdsJoined)
            {
                string path = Path.Combine(UnityEngine.Application.persistentDataPath, $"game_{gameId}.json");
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    GameData gameData = JsonUtility.FromJson<GameData>(json);

                    loadedGameIds.Add(gameId);
                    validGameNames.Add(gameData.gameName);
                }
            }

            if (validGameNames.Count > 0)
            {
                btn.interactable = true;
                text.text = "Continue";

                gameSelectDropdown.ClearOptions();
                gameSelectDropdown.AddOptions(validGameNames);
            }
            else
            {
                btn.interactable = false;
                text.text = "Continue (No Games)";
            }
        }
        else
        {
            btn.interactable = false;
            text.text = "Continue (No Profile)";
        }
    }



    public void OnCreateGamePressed()
    {
        NavigationManager.Instance.OpenPanel("Create Game - Panel");
    }

    public void OnJoinGamePressed()
    {

        NavigationManager.Instance.OpenPanel("Join Game - Panel");
    }

    public void OnContinuePressed()
    {
        NavigationManager.Instance.OpenPanel("Create Game - Panel");
    }

    public void OnConfirmContinue()
    {
        string selectedGameId = loadedGameIds[gameSelectDropdown.value];
        var game = SaveManager.LoadGame(selectedGameId);
        GameManager.Instance.currentGame = game;

        var user = GameManager.Instance.currentUser;

        if (game.gmUserIds.Contains(user.userId))
        {
            NavigationManager.Instance.OpenPanel("GM - Panel");
        }
        else
        {
            NavigationManager.Instance.OpenPanel("Player Game - Panel");
        }

        continuePanel.SetActive(false);
    }

    public void GoBackToMainMenu()
    {
        NavigationManager.Instance.OpenPanel("Main Menu - Panel");
    }

    public void Refresh()
    {
        // reload username, reload available games
    }
}
