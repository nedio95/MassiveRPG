using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class UserProfileUI : PanelBase
{
    public TMP_InputField usernameInput;
    public GameObject userProfilePanel;
    public GameObject mainMenuPanel;

    public TMP_Dropdown loadProfileDropdown;
    private List<string> userIds = new();

    public void Start()
    {
        PopulateSavedUsers(); 
    }

    public void OnCreateProfilePressed()
    {
        string username = usernameInput.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogWarning("Username cannot be empty.");
            return;
        }

        var newUser = new UserData
        {
            userId = System.Guid.NewGuid().ToString(),
            username = username,
            gameIdsJoined = new(),
            gameIdsCreated = new()
        };

        GameManager.Instance.currentUser = newUser;
        SaveManager.SaveUser(newUser);

        userProfilePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnLoadProfilePressed()
    {
        int index = loadProfileDropdown.value;
        if (index >= 0 && index < userIds.Count)
        {
            string selectedUserId = userIds[index];
            var user = SaveManager.LoadUser(selectedUserId);

            GameManager.Instance.currentUser = user;

            userProfilePanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    public void PopulateSavedUsers()
    {
        loadProfileDropdown.ClearOptions();
        userIds.Clear();

        var userFiles = Directory.GetFiles(Application.persistentDataPath, "user_*.json");
        List<string> usernames = new();

        foreach (var file in userFiles)
        {
            string json = File.ReadAllText(file);
            UserData data = JsonUtility.FromJson<UserData>(json);

            if (!string.IsNullOrEmpty(data.username))
            {
                usernames.Add(data.username);
                userIds.Add(data.userId);
            }
        }

        loadProfileDropdown.AddOptions(usernames);
    }



}
