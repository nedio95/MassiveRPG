using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.IO;

public class MainMenuUI : MonoBehaviour
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
        mainMenuPanel.SetActive(false);
        createGamePanel.SetActive(true);
    }

    public void OnJoinGamePressed()
    {

        mainMenuPanel.SetActive(false);
        joinGamePanel.SetActive(true);
    }

    public void OnContinuePressed()
    {
        mainMenuPanel.SetActive(false);
        continuePanel.SetActive(true);
    }

    public void OnConfirmContinue()
    {
        string selectedGameId = loadedGameIds[gameSelectDropdown.value];
        var game = SaveManager.LoadGame(selectedGameId);
        GameManager.Instance.currentGame = game;

        var user = GameManager.Instance.currentUser;

        if (game.gmUserIds.Contains(user.userId))
        {
            gmPanel.SetActive(true);
        }
        else
        {
            playerPanel.SetActive(true);
        }

        continuePanel.SetActive(false);
    }

    public void GoBackToMainMenu()
    {  
        
        createGamePanel.SetActive(false);
        joinGamePanel.SetActive(false);
        continuePanel.SetActive(false);
        playerPanel.SetActive(false);
        gmPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
